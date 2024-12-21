#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddDeliveryExceptionHandler(this IServiceCollection services)
        => services.AddExceptionHandler<GlobalExceptionHandler>();
}
