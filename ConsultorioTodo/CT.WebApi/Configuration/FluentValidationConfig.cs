﻿using CT.Manager.Validator;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;
using System.Text.Json.Serialization;

namespace CT.WebApi.Configuration;

public static class FluentValidationConfig
{
    public static void AddFluentValidationConfiguration(this IServiceCollection services)
    {
        services.AddControllers()
            .AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                x.SerializerSettings.Converters.Add(new StringEnumConverter());
            })
            .AddJsonOptions(p => p.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
            .AddFluentValidation(p =>
            {
                p.RegisterValidatorsFromAssemblyContaining<NovoClienteValidator>();
                p.RegisterValidatorsFromAssemblyContaining<NovoEnderecoValidator>();
                p.RegisterValidatorsFromAssemblyContaining<AlteraClienteValidator>();
                p.RegisterValidatorsFromAssemblyContaining<NovoTelefoneValidator>();
                p.RegisterValidatorsFromAssemblyContaining<NovoMedicoValidator>();
                p.RegisterValidatorsFromAssemblyContaining<AlteraMedicoValidator>();
                p.ValidatorOptions.LanguageManager.Culture = new CultureInfo("pt-BR");
            });

        services.AddFluentValidationRulesToSwagger();
    }
}
