#pragma warning disable IDE0130
using CustomCADs.Account.Application;
using Wolverine;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountApplication(this IServiceCollection services)
        => services
            .AddUseCases();

    private static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddWolverine(cfg => cfg.ApplicationAssembly = AccountApplicationReference.Assembly);
        
        return services;
    }
}
