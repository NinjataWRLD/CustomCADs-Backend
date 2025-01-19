using CustomCADs.Delivery.Application.Common.Exceptions;
using CustomCADs.Delivery.Domain.Common.Exceptions.Shipments;
using Microsoft.AspNetCore.Diagnostics;

namespace CustomCADs.Delivery.Endpoints.Common;

public class DeliveryExceptionHandler(IProblemDetailsService service) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken ct)
        => ex switch
        {
            ShipmentValidationException
                => await service.BadRequestResponseAsync(context, ex).ConfigureAwait(false),

            ShipmentAuthorizationException
                => await service.ForbidednResponseAsync(context, ex).ConfigureAwait(false),

            ShipmentNotFoundException
                => await service.NotFoundResponseAsync(context, ex).ConfigureAwait(false),

            _ => false
        };
}
