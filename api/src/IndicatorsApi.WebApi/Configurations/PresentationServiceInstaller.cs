using Carter;
using IndicatorsApi.WebApi.OptionsSetup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace IndicatorsApi.WebApi.Configurations;

/// <summary>
/// Presentation configuration.
/// </summary>
internal sealed class PresentationServiceInstaller
    : IServiceInstaller
{
    private const string Terms = "https://example.com/terms";
    private const string Contact = "https://example.com";
    private const string License = "https://opensource.org/licenses";

    /// <inheritdoc/>
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        OpenApiInfo openApi = new()
        {
            Title = "Indicators API",
            Version = "v1",
            Description = @"Indicators API is a RESTful API that allows you to create, read, update and delete indicators.",
            TermsOfService = new Uri(Terms),
            Contact = new()
            {
                Name = "Indicators API",
                Email = "juan.navia211@tau.usbmed.edu.co",
                Url = new Uri(Contact),
            },
            License = new()
            {
                Name = "USBMED",
                Url = new Uri(License),
            },
        };

        services
            .AddCarter();

        services
            .AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", openApi);

                OpenApiSecurityScheme securityScheme = new()
                {
                    Name = "JWT Authorization",
                    Description = "Enter JWT Bearer authorization token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Reference = new()
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme,
                    },
                };

                x.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

                OpenApiSecurityRequirement keyValues = new()
                {
                    { securityScheme, Array.Empty<string>() },
                };

                x.AddSecurityRequirement(keyValues);
            });

        services
            .AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder
                        .WithOrigins("*")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
    }
}
