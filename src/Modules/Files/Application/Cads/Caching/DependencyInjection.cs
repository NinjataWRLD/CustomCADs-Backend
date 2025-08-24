using CustomCADs.Files.Application.Cads.Caching;
using CustomCADs.Shared.Application.Abstractions.Cache;
using CustomCADs.Shared.Domain.TypedIds.Files;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
	public static IServiceCollection AddCadCaching(this IServiceCollection services)
		=> services.AddScoped<BaseCachingService<CadId, Cad>, CadCachingService>();
}
