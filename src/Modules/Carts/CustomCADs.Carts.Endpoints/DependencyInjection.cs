using CustomCADs.Carts.Endpoints.Common;


#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddCartsExceptionHandler(this IServiceCollection services)
        => services.AddExceptionHandler<CartsExceptionHandler>();
}
