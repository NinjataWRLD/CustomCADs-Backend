#pragma warning disable IDE0130
using CustomCADs.Customizations.Endpoints.Common;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomizationsExceptionHandler(this IServiceCollection services)
        => services.AddExceptionHandler<CustomizationsExceptionHandler>();
}
