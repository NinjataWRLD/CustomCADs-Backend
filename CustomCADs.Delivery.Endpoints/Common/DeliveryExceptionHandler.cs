using CustomCADs.Delivery.Application.Common.Exceptions;
using CustomCADs.Delivery.Domain.Common.Exceptions.Shipments;
using Microsoft.AspNetCore.Diagnostics;

namespace CustomCADs.Delivery.Endpoints.Common;

using static StatusCodes;

public class DeliveryExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken ct)
    {
        if (ex is ShipmentValidationException)
        {
            context.Response.StatusCode = Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Invalid Request Parameters",
                message = ex.Message,
            }, ct).ConfigureAwait(false);
        }
        else if (ex is ShipmentNotFoundException)
        {
            context.Response.StatusCode = Status404NotFound;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Resource Not Found",
                message = ex.Message
            }, ct).ConfigureAwait(false);
        }
        else if (ex is ShipmentAuthorizationException)
        {
            context.Response.StatusCode = Status403Forbidden;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Authorization Issue",
                message = ex.Message
            }, ct).ConfigureAwait(false);
        }

        return true;
    }
}
