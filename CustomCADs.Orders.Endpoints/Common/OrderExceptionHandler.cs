using CustomCADs.Orders.Application.Common.Exceptions.Completed;
using CustomCADs.Orders.Application.Common.Exceptions.Ongoing;
using CustomCADs.Orders.Domain.Common.Exceptions.CompletedOrder;
using CustomCADs.Orders.Domain.Common.Exceptions.OngoingOrders;
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
                => await service.ForbidednResponseAsync(context, ex).ConfigureAwait(false),

            CompletedOrderNotFoundException or OngoingOrderNotFoundException
                => await service.NotFoundResponseAsync(context, ex).ConfigureAwait(false),

            _ => false
        };
}
