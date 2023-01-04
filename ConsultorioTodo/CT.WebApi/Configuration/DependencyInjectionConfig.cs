using CT.Data.Repository;
using CT.Manager.Implementation;
using CT.Manager.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CT.WebApi.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IClienteManager, ClienteManager>();
        }
    }
}
