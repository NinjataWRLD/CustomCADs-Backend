﻿using CustomCADs.Accounts.Application;
using CustomCADs.Carts.Application;
using CustomCADs.Catalog.Application;
using CustomCADs.Categories.Application;
using CustomCADs.Delivery.Application;
using CustomCADs.Files.Application;
using CustomCADs.Identity.Application;
using CustomCADs.Identity.Domain.Entities;
using CustomCADs.Identity.Infrastructure;
using CustomCADs.Orders.Application;
using CustomCADs.Shared.Application.Requests.Middleware;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Infrastructure.Delivery;
using CustomCADs.Shared.Infrastructure.Email;
using CustomCADs.Shared.Infrastructure.Payment;
using CustomCADs.Shared.Infrastructure.Storage;
using FastEndpoints;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class ProgramExtensions
{
    private const string AuthScheme = JwtBearerDefaults.AuthenticationScheme;

    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        Assembly[] assemblies = [
            AccountApplicationReference.Assembly,
            CatalogApplicationReference.Assembly,
            CategoriesApplicationReference.Assembly,
            CartsApplicationReference.Assembly,
            DeliveryApplicationReference.Assembly,
            FilesApplicationReference.Assembly,
            OrdersApplicationReference.Assembly,
        ];

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
        services.AddValidatorsFromAssemblies(assemblies);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        services.AddScoped<IRequestSender, RequestSender>();

        return services;
    }

    public static IServiceCollection AddRaiserService(this IServiceCollection services)
    {
        services.AddEventRaiser([
            AccountApplicationReference.Assembly,
            CartsApplicationReference.Assembly,
            CatalogApplicationReference.Assembly,
            CategoriesApplicationReference.Assembly,
            IdentityApplicationReference.Assembly,
            OrdersApplicationReference.Assembly,
        ]);

        return services;
    }

    public static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<EmailSettings>(config.GetSection("Email"));
        services.AddEmailService();

        return services;
    }

    public static IServiceCollection AddPaymentService(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<PaymentSettings>(config.GetSection("Payment"));
        services.AddPaymentService();

        return services;
    }

    public static IServiceCollection AddDeliveryService(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<DeliverySettings>(config.GetSection("Delivery"));
        services.AddDeliveryService();

        return services;
    }

    public static IServiceCollection AddStorageService(this IServiceCollection services, IConfiguration config)
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

    public static IServiceCollection AddAccounts(this IServiceCollection services, IConfiguration config)
        => services
            .AddAccountsExceptionHandler()
            .AddAccountsPersistence(config);

    public static IServiceCollection AddCarts(this IServiceCollection services, IConfiguration config)
        => services
            .AddCartsExceptionHandler()
            .AddCartsPersistence(config);

    public static IServiceCollection AddCatalog(this IServiceCollection services, IConfiguration config)
        => services
            .AddCatalogExceptionHandler()
            .AddCatalogPersistence(config);

    public static IServiceCollection AddCategories(this IServiceCollection services, IConfiguration config)
        => services
            .AddCategoriesExceptionHandler()
            .AddCategoriesPersistence(config);

    public static IServiceCollection AddDelivery(this IServiceCollection services, IConfiguration config)
        => services
            .AddDeliveryExceptionHandler()
            .AddDeliveryPersistence(config);

    public static IServiceCollection AddFiles(this IServiceCollection services, IConfiguration config)
        => services
            .AddFilesPersistence(config);

    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration config)
        => services
            .AddIdentityExceptionHandler()
            .AddIdentityInfrastructure(config)
            .AddIdentityConfigs();

    public static IServiceCollection AddOrders(this IServiceCollection services, IConfiguration config)
        => services
            .AddOrdersExceptionHandler()
            .AddOrdersPersistence(config);

    public static async Task AddDbMigrationUpdater(this IServiceCollection services)
    {
        using IServiceScope scope = services.BuildServiceProvider().CreateScope();
        IServiceProvider provider = scope.ServiceProvider;

        await provider.UpdateAccountsContextAsync().ConfigureAwait(false);
        await provider.UpdateCartsContextAsync().ConfigureAwait(false);
        await provider.UpdateCatalogContextAsync().ConfigureAwait(false);
        await provider.UpdateCategoriesContextAsync().ConfigureAwait(false);
        await provider.UpdateDeliveryContextAsync().ConfigureAwait(false);
        await provider.UpdateFilesContextAsync().ConfigureAwait(false);
        await provider.UpdateIdentityContextAsync().ConfigureAwait(false);
        await provider.UpdateOrdersContextAsync().ConfigureAwait(false);
    }

    public static void AddEndpoints(this IServiceCollection services)
    {
        services.AddFastEndpoints();
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
    <li>Order and Purchase 3D Models</li>
    <li>Download them and have them Delivered</li>
    <li>Upload and Sell 3D Models</li>
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
        string clientUrl = config["URLs:Client"] ?? throw new KeyNotFoundException("Client Url not provided.");
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

    private static IServiceCollection AddIdentityConfigs(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.SignIn.RequireConfirmedEmail = true;
            options.SignIn.RequireConfirmedAccount = false;
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.User.RequireUniqueEmail = true;
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        })
        .AddEntityFrameworkStores<IdentityContext>()
        .AddDefaultTokenProviders();

        return services;
    }
}
