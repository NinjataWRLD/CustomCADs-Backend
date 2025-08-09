#pragma warning disable IDE0130
using CustomCADs.Identity.Application.Contracts;
using CustomCADs.Identity.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	public static void AddTokensService(this IServiceCollection services)
	{
		services.AddScoped<ITokenService, IdentityTokenService>();
	}
}
