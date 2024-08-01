using Microsoft.OpenApi.Models;
using System.Reflection;

namespace cadastroLogin.Configurantion;

public static class DependecyInjectionSwagger
{
    public static IServiceCollection AddInfrastructureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "Jwt",
                In = ParameterLocation.Header,
                Description = "Description",
            });
            c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
            {
                {
                new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                         Type = ReferenceType.SecurityScheme,
                          Id = "Bearer"
                    }

                },
                new string[] {}
                }
            });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);

        });
        return services;
    }

}
