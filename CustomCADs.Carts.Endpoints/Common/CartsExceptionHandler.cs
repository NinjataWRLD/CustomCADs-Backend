using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.Common.Exceptions.ActiveCarts.Carts;
using CustomCADs.Carts.Domain.Common.Exceptions.PurchasedCarts.Carts;
using Microsoft.AspNetCore.Diagnostics;

namespace CustomCADs.Carts.Endpoints.Common;

using static StatusCodes;

public class CartsExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken ct)
    {
        if (ex is ActiveCartValidationException or ActiveCartValidationException or PurchasedCartValidationException)
        {
            context.Response.StatusCode = Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Invalid Request Parameters",
                message = ex.Message,
            }, ct).ConfigureAwait(false);
        }
        else if (ex is Domain.Common.Exceptions.ActiveCarts.CartItems.ActiveCartItemNotFoundException)
        {
            context.Response.StatusCode = Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Invalid Request Parameters",
                message = ex.Message,
            }, ct).ConfigureAwait(false);
        }
        else if (ex is ActiveCartItemDeliveryException or ActiveCartAlreadyExistsException)
        {
            context.Response.StatusCode = Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Invalid Request Parameters",
                message = ex.Message,
            }, ct).ConfigureAwait(false);
        }
        else if (ex is PurchasedCartAuthorizationException)
        {
            context.Response.StatusCode = Status403Forbidden;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Authorization Issue",
                message = ex.Message
            }, ct).ConfigureAwait(false);
        }
        else if (ex is ActiveCartNotFoundException or ActiveCartItemNotFoundException or PurchasedCartNotFoundException or PurchasedCartItemNotFoundException)
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
