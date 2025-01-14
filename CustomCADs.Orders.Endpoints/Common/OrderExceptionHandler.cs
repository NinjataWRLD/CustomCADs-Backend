using CustomCADs.Orders.Application.Common.Exceptions.Completed;
using CustomCADs.Orders.Application.Common.Exceptions.Ongoing;
using CustomCADs.Orders.Domain.Common.Exceptions.CompletedOrder;
using CustomCADs.Orders.Domain.Common.Exceptions.OngoingOrders;
using Microsoft.AspNetCore.Diagnostics;

namespace CustomCADs.Orders.Endpoints.Common;

using static StatusCodes;

public class OrderExceptionHandler(IProblemDetailsService service) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken ct)
    {
        if (ex is CompletedOrderValidationException or OngoingOrderValidationException or OngoingOrderDesignerException or OngoingOrderDeliveryException or OngoingOrderCadException or OngoingOrderStatusException or OngoingOrderPriceException)
        {
            return await service.TryWriteAsync(new()
            {
                HttpContext = context,
                Exception = ex,
                ProblemDetails = new()
                {
                    Type = ex.GetType().Name,
                    Title = "Invalid Request Parameters",
                    Detail = ex.Message,
                    Status = Status400BadRequest,
                },
            }).ConfigureAwait(false);
        }
        else if (ex is CompletedOrderAuthorizationException or OngoingOrderAuthorizationException)
        {
            return await service.TryWriteAsync(new()
            {
                HttpContext = context,
                Exception = ex,
                ProblemDetails = new()
                {
                    Type = ex.GetType().Name,
                    Title = "Authorization Issue",
                    Detail = ex.Message,
                    Status = Status403Forbidden,
                },
            }).ConfigureAwait(false);
        }
        else if (ex is CompletedOrderNotFoundException or OngoingOrderNotFoundException)
        {
            return await service.TryWriteAsync(new()
            {
                HttpContext = context,
                Exception = ex,
                ProblemDetails = new()
                {
                    Type = ex.GetType().Name,
                    Title = "Resource Not Found",
                    Detail = ex.Message,
                    Status = Status404NotFound,
                },
            }).ConfigureAwait(false);
        }

        return true;
    }
}
