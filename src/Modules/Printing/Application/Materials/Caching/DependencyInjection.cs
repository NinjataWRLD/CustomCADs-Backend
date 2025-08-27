#pragma warning disable IDE0130
using CustomCADs.Printing.Application.Materials.Caching;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
	public static IServiceCollection AddMaterialCaching(this IServiceCollection services)
		=> services.AddScoped<BaseCachingService<MaterialId, Material>, MaterialCachingService>();
}
