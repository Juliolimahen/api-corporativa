using CT.Manager.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace CT.WebApi.Configuration
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(NovoClienteMappingProfile), typeof(AlteraClienteMappingProfile));
        }
    }
}
