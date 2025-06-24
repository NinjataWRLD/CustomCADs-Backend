using CustomCADs.Accounts.Application.Roles.Caching;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class CachingDependencyInjection
{
	public static IServiceCollection AddRoleCaching(this IServiceCollection services)
		=> services.AddScoped<BaseCachingService<RoleId, Role>, RolesCachingService>();
}
