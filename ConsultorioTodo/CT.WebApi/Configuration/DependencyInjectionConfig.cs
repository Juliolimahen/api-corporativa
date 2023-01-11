using CT.Data.Repository;
using CT.Manager.Implementation;
using CT.Manager.Interfaces.Managers;
using CT.Manager.Interfaces.Repositories;
using CT.Manager.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace CT.WebApi.Configuration;

public static class DependencyInjectionConfig
{
    public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IClienteManager, ClienteManager>();
        services.AddScoped<IMedicoRepository, MedicoRepository>();
        services.AddScoped<IMedicoManager, MedicoManager>();
        services.AddScoped<IEspecialidadeRepository, EspecialidadeRepository>();
        //services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        //services.AddScoped<IUsuarioManager, UsuarioManager>();
    }
}
