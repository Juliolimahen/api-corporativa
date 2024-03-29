using CT.Data.Context;
using CT.Data.Repository;
using CT.Manager.Implementation;
using CT.Manager.Interfaces;
using CT.Manager.Mappings;
using CT.Manager.Validator;
using CT.WebApi.Configuration;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CT.WebApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddJwtTConfiguration(Configuration);

        services.AddFluentValidationConfiguration();

        services.AddAutoMapperConfiguration();

        services.AddDataBaseConfiguration(Configuration);

        services.AddDependencyInjectionConfiguration();

        services.AddSwaggerConfiguration();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseExceptionHandler("/error");

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseDataBaseConfiguration();

        app.UseSwaggerConfiguration();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseJwtConfiguration();

        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}