using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repository;

namespace Persistence
{
    public static class ServiceExtension
    {
        public static void AddPersistenceInfraestructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                ));

            #region Repositories
             services.AddTransient(typeof(IRepositoryAsync<>), typeof(MyRepositoryAsync<>));// Dependency Injection Generic
            #endregion
        }
    }
}
