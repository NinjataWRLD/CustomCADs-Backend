using CustomCADs.Orders.Application.Common.Exceptions.Completed;
using CustomCADs.Orders.Application.Common.Exceptions.Ongoing;
using CustomCADs.Orders.Domain.CompletedOrders.Exceptions;
using CustomCADs.Orders.Domain.OngoingOrders.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace CustomCADs.Orders.Endpoints.Common;

public class OrderExceptionHandler(IProblemDetailsService service) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken ct)
        => ex switch
        {
            CompletedOrderValidationException or OngoingOrderValidationException or OngoingOrderDesignerException or OngoingOrderDeliveryException or OngoingOrderCadException or OngoingOrderStatusException or OngoingOrderPriceException
                => await service.BadRequestResponseAsync(context, ex).ConfigureAwait(false),

            CompletedOrderAuthorizationException or OngoingOrderAuthorizationException
                => await service.ForbiddenResponseAsync(context, ex).ConfigureAwait(false),

            CompletedOrderNotFoundException or OngoingOrderNotFoundException
                => await service.NotFoundResponseAsync(context, ex).ConfigureAwait(false),

            _ => false
        };
}
