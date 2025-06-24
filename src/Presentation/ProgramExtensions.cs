using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Presentation;
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

	public static IServiceCollection AddUseCases(this IServiceCollection services, IWebHostEnvironment env)
	{
		services.AddMessagingServices(
			codeGen: !env.IsDevelopment(),
			entry: CustomCADs.Tools.CodeGen.CodeGenReference.Assembly,
			assemblies: [
				CustomCADs.Accounts.Application.AccountApplicationReference.Assembly,
				CustomCADs.Carts.Application.CartsApplicationReference.Assembly,
				CustomCADs.Catalog.Application.CatalogApplicationReference.Assembly,
				CustomCADs.Categories.Application.CategoriesApplicationReference.Assembly,
				CustomCADs.Customizations.Application.CustomizationsApplicationReference.Assembly,
				CustomCADs.Customs.Application.CustomsApplicationReference.Assembly,
				CustomCADs.Delivery.Application.DeliveryApplicationReference.Assembly,
				CustomCADs.Files.Application.FilesApplicationReference.Assembly,
				CustomCADs.Identity.Application.IdentityApplicationReference.Assembly,
			]
		);

		return services;
	}

	public static IServiceCollection AddCache(this IServiceCollection services)
	{
		services.AddCacheService();
		services.AddMaterialCaching();
		services.AddCategoryCaching();

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

	public static IServiceCollection AddGlobalExceptionHandler(this IServiceCollection services)
		=> services.AddExceptionHandler<GlobalExceptionHandler>();

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

	public static void AddApiDocumentation(this IServiceCollection services, string version = "v1")
	{
		services.AddOpenApi(version, cfg =>
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
					Version = version
				};
				document.Tags = [.. document.Tags.OrderBy(t => t.Name)];

				return Task.CompletedTask;
			});
		});
	}

	public static void AddCorsForClient(this IServiceCollection services, IConfiguration config)
	{
		services.Configure<CookieSettings>(config.GetSection("Cookie"));

		IConfigurationSection section = config.GetSection("ClientURLs");
		services.Configure<ClientUrlSettings>(section);

		services.AddCors(opt =>
		{
			ClientUrlSettings settings = section.Get<ClientUrlSettings>()
				?? throw new KeyNotFoundException("URLs not provided.");

			opt.AddDefaultPolicy(builder =>
			{
				string[] urls = settings.All.Split(',');
				builder.WithOrigins(urls)
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

	public static void UseCorsForClient(this IApplicationBuilder app)
	{
		app.UseCors();
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
		app.MapScalarApiReference(uiPattern, options =>
		{
			ScalarTheme[] themes =
			[
				ScalarTheme.BluePlanet,
				ScalarTheme.Kepler,
				ScalarTheme.Mars,
				ScalarTheme.DeepSpace,
			];

			options
				.WithOpenApiRoutePattern(apiPattern)
				.WithOperationSorter(OperationSorter.Method)
				.WithTitle("CustomCADs API")
				.WithTheme(themes[Random.Shared.Next(0, themes.Length)])
				.WithFavicon("/favicon.ico")
				.WithDarkModeToggle(false);
		});

		return app;
	}
}
