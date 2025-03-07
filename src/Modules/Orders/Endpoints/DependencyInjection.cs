﻿using CustomCADs.Orders.Endpoints.Common;


#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddOrdersExceptionHandler(this IServiceCollection services)
        => services.AddExceptionHandler<OrderExceptionHandler>();
}
