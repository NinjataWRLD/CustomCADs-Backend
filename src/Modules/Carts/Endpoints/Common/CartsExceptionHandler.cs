using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.ActiveCarts.Exceptions.CartItems;
using CustomCADs.Carts.Domain.ActiveCarts.Exceptions.Carts;
using CustomCADs.Carts.Domain.PurchasedCarts.Exceptions.CartItems;
using CustomCADs.Carts.Domain.PurchasedCarts.Exceptions.Carts;
using Microsoft.AspNetCore.Diagnostics;

namespace CustomCADs.Carts.Endpoints.Common;

public class CartsExceptionHandler(IProblemDetailsService service) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken ct)
        => ex switch
        {
            ActiveCartValidationException or ActiveCartItemValidationException or PurchasedCartValidationException or PurchasedCartItemValidationException or ActiveCartItemDeliveryException or ActiveCartAlreadyExistsException
                => await service.BadRequestResponseAsync(context, ex).ConfigureAwait(false),

            PurchasedCartAuthorizationException
                => await service.ForbiddenResponseAsync(context, ex).ConfigureAwait(false),

            ActiveCartNotFoundException or ActiveCartItemNotFoundException or PurchasedCartNotFoundException or PurchasedCartItemNotFoundException
                => await service.NotFoundResponseAsync(context, ex).ConfigureAwait(false),

            _ => false
        };
}
