using CustomCADs.Identity.Domain.Users;
using CustomCADs.Identity.Infrastructure.Identity;
using CustomCADs.Identity.Infrastructure.Identity.ShadowEntities;
using CustomCADs.Printing.Domain.Services;
using Microsoft.AspNetCore.Identity;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static UserConstants;

public static class ProgramExtensions
{
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

	public static async Task ExecuteDbMigrationUpdater(this IServiceCollection services)
	{
		using IServiceScope scope = services.BuildServiceProvider().CreateScope();
		IServiceProvider provider = scope.ServiceProvider;

		await Task.WhenAll([
			provider.UpdateAccountsContextAsync(),
			provider.UpdateCartsContextAsync(),
			provider.UpdateCatalogContextAsync(),
			provider.UpdatePrintingContextAsync(),
			provider.UpdateCustomsContextAsync(),
			provider.UpdateDeliveryContextAsync(),
			provider.UpdateFilesContextAsync(),
			provider.UpdateIdempotencyContextAsync(),
			provider.UpdateIdentityContextAsync(),
		]).ConfigureAwait(false);
	}
}
