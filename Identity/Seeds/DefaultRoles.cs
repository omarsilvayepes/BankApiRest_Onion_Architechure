using Identity.Models;
using Microsoft.AspNetCore.Identity;


namespace Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles

            await roleManager.CreateAsync(new IdentityRole(Application.Enums.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Application.Enums.Roles.Basic.ToString()));

        }
    }
}
