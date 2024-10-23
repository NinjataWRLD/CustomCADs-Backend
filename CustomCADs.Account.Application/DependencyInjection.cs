#pragma warning disable IDE0130
using CustomCADs.Account.Application;
using Wolverine;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddMediator(this IServiceCollection services)
    {
        services.AddWolverine(cfg => cfg.ApplicationAssembly = AccountApplicationReference.Assembly);
    }
}
