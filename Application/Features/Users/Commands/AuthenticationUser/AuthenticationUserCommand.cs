using Application.DTOs.Users;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Users.Commands.AuthenticationUser
{
    public class AuthenticationUserCommand:IRequest<Response<AuthenticationResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string IpAddress { get; set; }
    }

    public class AuthenticateCommandHandler : IRequestHandler<AuthenticationUserCommand, Response<AuthenticationResponse>>
    {
        private readonly IAccountService accountService;

        public AuthenticateCommandHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async  Task<Response<AuthenticationResponse>> Handle(AuthenticationUserCommand request, CancellationToken cancellationToken)
        {
            return await accountService.AuthenticationAsync(new AuthenticationRequest {Email=request.Email,Password=request.Password }, request.IpAddress);
        }
    }
}
