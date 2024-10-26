#pragma warning disable IDE0130
using CustomCADs.Auth;
using CustomCADs.Auth.Application.Contracts;
using CustomCADs.Auth.Application.Dtos;
using CustomCADs.Auth.Application.Services;
using CustomCADs.Auth.Extensions;
using CustomCADs.Auth.Infrastructure;
using CustomCADs.Auth.Infrastructure.Entities;
using CustomCADs.Shared.Infrastructure.Email;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Wolverine;

namespace Microsoft.Extensions.DependencyInjection;

public static class ProgramExtensions
{
    private static void AddIdentityContext(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("AuthConnection")
                ?? throw new KeyNotFoundException("Could not find connection string 'AuthConnection'.");
        services.AddDbContext<AuthContext>(options => options.UseSqlServer(connectionString));
    }

    private static void AddIdentityServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, AppUserService>();
        services.AddScoped<IRoleService, AppRoleService>();
    }

    public static void AddMessageBus(this IServiceCollection services)
    {
        services.AddWolverine(cfg => cfg.Discovery
            .IncludeAssembly(AuthReference.Assembly));
    }

    private static void AddTokenService(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ITokenService, AppTokenService>();
        services.Configure<JwtSettings>(config.GetSection("JwtSettings"));
    }

    public static void AddIdentityAuth(this IServiceCollection services, IConfiguration config)
    {
        services.AddIdentityContext(config);
        services.AddIdentityServices();
        services.AddTokenService(config);

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
        .AddEntityFrameworkStores<AuthContext>()
        .AddDefaultTokenProviders();
    }

    public static void AddEmail(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<EmailOptions>(config.GetSection("Email"));
        services.AddEmailServices();
    }

    public static void AddEndpoints(this IServiceCollection services)
    {
        services.AddFastEndpoints();
        services.AddEndpointsApiExplorer();
    }

    public static void AddApiDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new()
            {
                Title = "CustomCADs Auth API",
                Description = "An API for AuthN/AuthZ related operations.",
                Contact = new() { Name = "Ivan", Email = "ivanangelov414@gmail.com" },
                License = new() { Name = "Apache License 2.0", Url = new("https://www.apache.org/licenses/LICENSE-2.0") },
                Version = "v1"
            });
        });
    }

    public static void AddAuthAndJwt(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(opt =>
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

    public static void AddRoles(this IServiceCollection services, IEnumerable<string> roles)
    {
        services.AddAuthorization(options =>
        {
            foreach (string role in roles)
            {
                options.AddPolicy(role, policy => policy.RequireRole(role));
            }
        });
    }

    public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var ehf = context.Features.Get<IExceptionHandlerFeature>();
                var ex = ehf?.Error;

                if (ex != null)
                {
                    await GlobalExceptionHandler
                        .TryHandleAsync(context, ex, context.RequestAborted)
                        .ConfigureAwait(false);
                }
            });
        });
    }

    public static void UseEndpoints(this IApplicationBuilder app)
    {
        app.UseFastEndpoints(cfg =>
        {
            cfg.Endpoints.RoutePrefix = "API";
            cfg.Versioning.DefaultVersion = 1;
            cfg.Versioning.PrependToRoute = true;
        });
    }
}
