using Application.Enums;
using Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Seeds
{
    public static class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default Baisc User

            var defaultUser = new ApplicationUser()
            {
                UserName = "userBasic",
                Email = "userBasic@mail.com",
                Name = "Pepito",
                LastName = "Perez",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(user => user.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$word");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                }

            }

        }
    }
}
