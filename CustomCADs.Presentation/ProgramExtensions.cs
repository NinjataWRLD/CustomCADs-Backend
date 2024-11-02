using CustomCADs.Account.Application;
using CustomCADs.Account.Endpoints;
using CustomCADs.Auth.Application;
using CustomCADs.Auth.Application.Contracts;
using CustomCADs.Auth.Endpoints;
using CustomCADs.Catalog.Application;
using CustomCADs.Catalog.Endpoints;
using CustomCADs.Presentation;
using CustomCADs.Shared.Infrastructure.Email;
using CustomCADs.Shared.Infrastructure.Payment;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Wolverine;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class ProgramExtensions
{
    private const string AuthScheme = JwtBearerDefaults.AuthenticationScheme;

    public static IServiceCollection AddSignInManager(this IServiceCollection services)
    {
        services.AddScoped<ISignInService, AppSignInService>();

        return services;
    }

    public static IServiceCollection AddBus(this IServiceCollection services)
    {
        services.AddWolverine(cfg =>
        {
            cfg.Discovery
                .IncludeAssembly(AccountApplicationReference.Assembly)
                .IncludeAssembly(AuthApplicationReference.Assembly)
                .IncludeAssembly(CatalogApplicationReference.Assembly);
        });

        return services;
    }

    public static IServiceCollection AddEmail(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<EmailOptions>(config.GetSection("Email"));
        services.AddEmailServices();

        return services;
    }

    public static IServiceCollection AddPayment(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<StripeKeys>(config.GetSection("Stripe"));
        services.AddPaymentServices();

        return services;
    }

    public static void AddAuthNAndJwt(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = AuthScheme;
            opt.DefaultForbidScheme = AuthScheme;
            opt.DefaultSignInScheme = AuthScheme;
            opt.DefaultSignOutScheme = AuthScheme;
            opt.DefaultChallengeScheme = AuthScheme;
            opt.DefaultScheme = AuthScheme;
        }).AddJwtBearer(opt =>
        {
            string? secretKey = config["JwtSettings:SecretKey"];
            ArgumentNullException.ThrowIfNull(secretKey, nameof(secretKey));

            opt.TokenValidationParameters = new()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config["JwtSettings:Issuer"],
                ValidAudience = config["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            };

            opt.Events = new()
            {
                OnMessageReceived = context =>
                {
                    context.Token = context.Request.Cookies["jwt"];
                    return Task.CompletedTask;
                },
            };
        });
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

    public static void AddApiDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new()
            {
                Title = "CustomCADs API",
                Description = "An API to Order, Purchase, Upload and Validate 3D Models",
                Contact = new() { Name = "Ivan", Email = "ivanangelov414@gmail.com" },
                License = new() { Name = "Apache License 2.0", Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0") }
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

    public static void UseSwagger(this IApplicationBuilder app, string name)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", name);
            c.RoutePrefix = string.Empty;
        });
    }
}
