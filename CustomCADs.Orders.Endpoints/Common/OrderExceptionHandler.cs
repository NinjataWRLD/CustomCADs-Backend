using CustomCADs.Orders.Application.Common.Exceptions.Completed;
using CustomCADs.Orders.Application.Common.Exceptions.Ongoing;
using CustomCADs.Orders.Domain.Common.Exceptions.CompletedOrder;
using CustomCADs.Orders.Domain.Common.Exceptions.OngoingOrders;
using Microsoft.AspNetCore.Diagnostics;

namespace CustomCADs.Orders.Endpoints.Common;

using static StatusCodes;

public class OrderExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken ct)
    {
        if (ex is CompletedOrderValidationException or OngoingOrderValidationException or OngoingOrderDesignerException or OngoingOrderDeliveryException or OngoingOrderCadException)
        {
            context.Response.StatusCode = Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Invalid Request Parameters",
                message = ex.Message,
            }, ct).ConfigureAwait(false);
        }
        else if (ex is CompletedOrderAuthorizationException or OngoingOrderAuthorizationException)
        {
            context.Response.StatusCode = Status403Forbidden;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Authorization Issue",
                message = ex.Message
            }, ct).ConfigureAwait(false);
        }
        else if (ex is CompletedOrderNotFoundException or OngoingOrderNotFoundException)
        {
            context.Response.StatusCode = Status404NotFound;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Resource Not Found",
                message = ex.Message
            }, ct).ConfigureAwait(false);
        }

        return true;
    }
}
