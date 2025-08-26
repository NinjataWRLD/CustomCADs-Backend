using CustomCADs.Files.Application.Cads.Caching;


#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
	public static IServiceCollection AddCadCaching(this IServiceCollection services)
		=> services.AddScoped<BaseCachingService<CadId, Cad>, CadCachingService>();
}
