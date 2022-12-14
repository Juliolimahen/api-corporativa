using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace CT.WebApi.Configuration;

public static class SwaggerConfig
{
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Consultório Todo",
                Version = "v1",
                Description = "API da aplicação consultório Todo.",
                Contact = new OpenApiContact
                {
                    Name = "Julio Lima",
                    Email = "juliolima_henrique@outlook.com",
                    Url = new Uri("https://github.com/juliolimahen")
                },
                License = new OpenApiLicense
                {
                    Name = "OSD",
                    Url = new Uri("https://opensource.org/osd")
                },
                TermsOfService = new Uri("https://opensource.org/osd")
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
            xmlPath = Path.Combine(AppContext.BaseDirectory, "CT.Core.Shared.xml");
            c.IncludeXmlComments(xmlPath);

            //c.AddFluentValidationRulesScoped();
            //c.AddFluentValidationRules();
        });
    }
    public static void UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.RoutePrefix = string.Empty;
            c.SwaggerEndpoint("./swagger/v1/swagger.json", "CT v1");
        });
    }
}
