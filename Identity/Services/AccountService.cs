using Application.DTOs.Users;
using Application.Enums;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Settings;
using Identity.Helpers;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            JWTSettings jwtSettings,
            IDateTimeService dateTimeService
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwtSettings= jwtSettings;
            _dateTimeService = dateTimeService;
        }

        public async Task<Response<AuthenticationResponse>> AuthenticationAsync(AuthenticationRequest request, string ipAddress)
        {
            var user=await _userManager.FindByEmailAsync(request.Email);
            if(user == null) { throw new ApiException($"There is Not an Account Registered with the email: {request.Email}"); }

            var result = await _signInManager.PasswordSignInAsync(user.UserName,request.Password,false,lockoutOnFailure:false);
            if(!result.Succeeded) { throw new ApiException($"Invalid Credentials: {request.Email}"); }

            JwtSecurityToken jwtSecurityToken = await GenerateJWTAsync(user);
            var roleList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            var refreshToken = GenerateRefreshToken(ipAddress);
            AuthenticationResponse response = new()
            {
                Id=user.Id,
                JWToken=new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email=user.Email,
                UserName=user.UserName,
                Roles= roleList.ToList(),
                IsVerified=user.EmailConfirmed,
                RefreshToken=refreshToken.Token
            };

            return new Response<AuthenticationResponse>(response,$"Authenticate User{user.UserName}");
        }

        private async Task<JwtSecurityToken>GenerateJWTAsync(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles=await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for(int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid",user.Id),
                new Claim("ip",ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurity = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredential=new SigningCredentials(symmetricSecurity,SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer:_jwtSettings.Issuer,
                audience:_jwtSettings.Audience,
                claims:claims,
                expires:DateTime.Now.AddMinutes(_jwtSettings.DurationMinutes),
                signingCredentials:signingCredential
                );
            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token=RandomTokenString(),
                Expires=DateTime.Now.AddDays(7),
                Created=DateTime.Now,
                CreatedByIp=ipAddress
            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return BitConverter.ToString(randomBytes).Replace("-","");
        }

        public async Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            var sameUserName=await _userManager.FindByNameAsync(request.UserName);
            if (sameUserName != null)
            {
                throw new ApiException($"The UserName{request.UserName} it is already register");
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                Name = request.Name,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed=true,
                PhoneNumberConfirmed=true
            };

            var sameUserEmail=await _userManager.FindByEmailAsync(request.Email);
            if(sameUserEmail != null) 
            {
                throw new ApiException($"The UserEmail{request.Email} it is already register");
            }
            else
            {

                var result=await _userManager.CreateAsync(user,request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user,Roles.Basic.ToString());// Rol by default is Basic
                    return new Response<string>(user.Id,message:$"User registered Succesfully. {request.UserName}");
                }
                else
                {
                    throw new ApiException($"{result.Errors}");
                }
            }
        }
    }
}
