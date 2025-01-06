using CustomCADs.Orders.Application.Common.Exceptions;
using CustomCADs.Orders.Domain.Common.Exceptions.Orders;
using Microsoft.AspNetCore.Diagnostics;

namespace CustomCADs.Orders.Endpoints.Common;

using static StatusCodes;

public class OrderExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken ct)
    {
        if (ex is OrderValidationException or OrderDesignerException or OrderDeliveryException or OrderCadException or OrderStatusException)
        {
            context.Response.StatusCode = Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Invalid Request Parameters",
                message = ex.Message,
            }, ct).ConfigureAwait(false);
        }
        else if (ex is OrderAuthorizationException)
        {
            context.Response.StatusCode = Status403Forbidden;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Authorization Issue",
                message = ex.Message
            }, ct).ConfigureAwait(false);
        }
        else if (ex is OrderNotFoundException)
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
