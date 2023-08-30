using Identity.Models;
using Identity.Seeds;
using Microsoft.AspNetCore.Identity;
using WebApi;

public class Program
{
    public  async static Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await DefaultRoles.SeedAsync(userManager, roleManager);
                await DefaultAdminUser.SeedAsync(userManager, roleManager);
                await DefaultBasicUser.SeedAsync(userManager, roleManager);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
}