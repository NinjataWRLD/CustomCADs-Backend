﻿using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddCatalog(this IServiceCollection services, IConfiguration config)
        => services
            .AddExceptionHandler<GlobalExceptionHandler>()
            .AddCatalogPersistence(config);
}
