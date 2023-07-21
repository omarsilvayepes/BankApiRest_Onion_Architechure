using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Shared.Services;

namespace Shared
{
    public static class ServiceExtension
    {
        public static void AddSharedInfraestructure(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();
        }
    }
}
