using CustomCADs.Delivery.Infrastructure;
using CustomCADs.Files.Infrastructure;
using CustomCADs.Identity.Domain.Users;
using CustomCADs.Identity.Infrastructure.Identity;
using CustomCADs.Identity.Infrastructure.Identity.ShadowEntities;
using CustomCADs.Identity.Infrastructure.Tokens;
using CustomCADs.Printing.Domain.Services;
using CustomCADs.Shared.Infrastructure.Email;
using CustomCADs.Shared.Infrastructure.Payment;
using Microsoft.AspNetCore.Identity;


#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static UserConstants;

public static class ProgramExtensions
{
	public static IServiceCollection GenerateUseCases(this IServiceCollection services, IWebHostEnvironment env)
	{
		services.AddMessagingServices(
			codeGen: true,
			entry: CustomCADs.Tools.CodeGen.CodeGenReference.Assembly,
			assemblies: [
				CustomCADs.Accounts.Application.AccountApplicationReference.Assembly,
				CustomCADs.Carts.Application.CartsApplicationReference.Assembly,
				CustomCADs.Catalog.Application.CatalogApplicationReference.Assembly,
				CustomCADs.Printing.Application.PrintingApplicationReference.Assembly,
				CustomCADs.Customs.Application.CustomsApplicationReference.Assembly,
				CustomCADs.Delivery.Application.DeliveryApplicationReference.Assembly,
				CustomCADs.Files.Application.FilesApplicationReference.Assembly,
				CustomCADs.Idempotency.Application.IdempotencyApplicationReference.Assembly,
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
		services.AddRoleCaching();

		return services;
	}

	public static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration config)
	{
		services.Configure<EmailSettings>(config.GetSection("Email"));
		services.AddEmailService();

		return services;
	}

	public static IServiceCollection AddTokensService(this IServiceCollection services, IConfiguration config)
	{
		services.Configure<JwtSettings>(config.GetSection("Jwt"));
		services.AddTokensService();

		return services;
	}

	public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration config)
	{
		const string connectionStringKey = "ApplicationConnection";
		string? connectionString = config.GetConnectionString(connectionStringKey)
			?? throw new KeyNotFoundException($"Could not find connection string '{connectionStringKey}'.");

		services.AddIdentityServices(connectionString);

		return services;
	}

	public static IServiceCollection AddPaymentService(this IServiceCollection services, IConfiguration config)
	{
		IConfigurationSection section = config.GetSection("Payment");
		services.Configure<PaymentSettings>(section);

		Stripe.StripeConfiguration.ApiKey = section.Get<PaymentSettings>()?.SecretKey;
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

		services.AddCadStorageService();
		services.AddImageStorageService();

		return services;
	}

	public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration config)
	{
		services.AddIdentity<AppUser, AppRole>(options =>
		{
			options.SignIn.RequireConfirmedEmail = true;
			options.SignIn.RequireConfirmedAccount = false;
			options.Password.RequireDigit = false;
			options.Password.RequireNonAlphanumeric = false;
			options.Password.RequireLowercase = false;
			options.Password.RequireUppercase = false;
			options.Password.RequiredLength = PasswordMinLength;
			options.User.RequireUniqueEmail = true;
			options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+" + ' '; // default + space
			options.Lockout.MaxFailedAccessAttempts = 5;
			options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
		})
		.AddEntityFrameworkStores<IdentityContext>()
		.AddDefaultTokenProviders();

		const string connectionStringKey = "ApplicationConnection";
		string? connectionString = config.GetConnectionString(connectionStringKey)
			?? throw new KeyNotFoundException($"Could not find connection string '{connectionStringKey}'.");

		services.AddIdentityServices(connectionString);

		return services;
	}

	public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
		=> services
			.AddAccountsPersistence(config)
			.AddCartsPersistence(config)
			.AddCatalogPersistence(config)
			.AddPrintingPersistence(config)
			.AddCustomsPersistence(config)
			.AddDeliveryPersistence(config)
			.AddFilesPersistence(config)
			.AddIdempotencyPersistence(config);


	public static IServiceCollection AddDomainServices(this IServiceCollection services)
	{
		services.AddScoped<IPrintCalculator, PrintCalculator>();

		return services;
	}

	public static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
	{
		services.AddCatalogBackgroundJobs();
		services.AddIdempotencyBackgroundJobs();

		return services;
	}
}
