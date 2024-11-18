using CustomCADs.Account.Application;
using CustomCADs.Account.Endpoints;
using CustomCADs.Auth.Application;
using CustomCADs.Auth.Application.Contracts;
using CustomCADs.Auth.Endpoints;
using CustomCADs.Catalog.Application;
using CustomCADs.Catalog.Endpoints;
using CustomCADs.Orders.Application;
using CustomCADs.Presentation;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Infrastructure.Email;
using CustomCADs.Shared.Infrastructure.Payment;
using CustomCADs.Shared.Infrastructure.Storage;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Scalar.AspNetCore;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class ProgramExtensions
{
    private const string AuthScheme = JwtBearerDefaults.AuthenticationScheme;

    public static IServiceCollection AddSignInService(this IServiceCollection services)
    {
        services.AddScoped<ISignInService, AppSignInService>();

        return services;
    }

    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IRequestSender, RequestSender>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies([
            CatalogApplicationReference.Assembly,
            AccountApplicationReference.Assembly,
            OrdersApplicationReference.Assembly,
        ]));

        return services;
    }

    public static IServiceCollection AddCache(this IServiceCollection services)
    {
        services.AddCacheService();

        return services;
    }

    public static IServiceCollection AddRaiser(this IServiceCollection services)
    {
        services.AddEventRaiser([
            AccountApplicationReference.Assembly,
            AuthApplicationReference.Assembly,
            CatalogApplicationReference.Assembly,
            OrdersApplicationReference.Assembly,
        ]);

        return services;
    }

    public static IServiceCollection AddEmail(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<EmailSettings>(config.GetSection("Email"));
        services.AddEmailService();

        return services;
    }

    public static IServiceCollection AddPayment(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<PaymentSettings>(config.GetSection("Payment"));
        services.AddPaymentService();

        return services;
    }

    public static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<StorageSettings>(config.GetSection("Storage"));
        services.AddStorageService();

        return services;
    }

    public static void AddAuthZ(this IServiceCollection services, IEnumerable<string> roles)
    {
        services.AddAuthorization(options =>
        {
            foreach (string role in roles)
            {
                options.AddPolicy(role, policy => policy.RequireRole(role));
            }
        });
    }

    public static void AddEndpoints(this IServiceCollection services)
    {
        services.AddFastEndpoints(cfg =>
        {
            cfg.Assemblies =
            [
                AccountEndpointsReference.Assembly,
                AuthEndpointsReference.Assembly,
                CatalogEndpointsReference.Assembly,
            ];
        });
        services.AddEndpointsApiExplorer();
    }

    public static void AddJsonOptions(this IServiceCollection services)
    {
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
    }

    public static void AddApiDocumentation(this IServiceCollection services)
    {
        services.AddOpenApi(cfg =>
        {
            cfg.AddDocumentTransformer((document, context, ct) =>
            {
                string description = """
**The best API to**: 
<ul> 
<li>Order 3D Models and Track Deliveries</li>
<li>Upload and Sell 3D Models</li>
<li>Take Orders and Validate 3D Models</li>
<li>Administer the Whole System.</li>
</ul>
""";

                document.Info = new()
                {
                    Title = "CustomCADs API",
                    Description = description,
                    Contact = new() { Name = "Ivan", Email = "ivanangelov414@gmail.com", },
                    License = new() { Name = "Apache License 2.0", Url = new("https://www.apache.org/licenses/LICENSE-2.0"), },
                    Version = "v1"
                };
                document.Tags = [.. document.Tags.OrderBy(t => t.Name)];

                return Task.CompletedTask;
            });
        });
    }

    public static void AddCorsForClient(this IServiceCollection services, IConfiguration config)
    {
        string clientUrl = config["URLs:Client"] ?? throw new ArgumentNullException("No Client URL provided.");
        services.AddCors(opt =>
        {
            opt.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins(clientUrl)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
            });
        });
    }

    public static void LimitUploadSize(this IWebHostBuilder webhost, int limit = 300_000_000)
    {
        webhost.ConfigureKestrel(o => o.Limits.MaxRequestBodySize = limit);
    }

    public static IApplicationBuilder UseEndpoints(this IApplicationBuilder app)
    {
        app.UseFastEndpoints(cfg =>
        {
            cfg.Endpoints.Configurator = ep => ep.AuthSchemes(AuthScheme);
            cfg.Endpoints.RoutePrefix = "api";
            cfg.Versioning.DefaultVersion = 1;
            cfg.Versioning.PrependToRoute = true;
        });
        
        return app;
    }

    public static IEndpointRouteBuilder MapApiDocumentationUi(this IEndpointRouteBuilder app, [StringSyntax("Route")] string apiPattern = "/openai/{documentName}.json", [StringSyntax("Route")] string uiPattern = "/scalar/{documentName}")
    {
        app.MapOpenApi(apiPattern);
        app.MapScalarApiReference(options =>
        {
            ScalarTheme[] themes =
            [
                ScalarTheme.Solarized,
                ScalarTheme.BluePlanet,
                ScalarTheme.Kepler,
                ScalarTheme.Mars,
                ScalarTheme.DeepSpace,
            ];

            options
                .WithOpenApiRoutePattern(apiPattern)
                .WithEndpointPrefix(uiPattern)
                .WithOperationSorter(OperationSorter.Alpha)
                .WithTitle("CustomCADs API")
                .WithTheme(themes[Random.Shared.Next(0, themes.Length)])
                .WithFavicon("/favicon.ico")
                .WithDarkModeToggle(false);
        });

        return app;
    }
}
