using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.Common.Exceptions.ActiveCarts.Carts;
using CustomCADs.Carts.Domain.Common.Exceptions.PurchasedCarts.Carts;
using Microsoft.AspNetCore.Diagnostics;

namespace CustomCADs.Carts.Endpoints.Common;

using static StatusCodes;

public class CartsExceptionHandler(IProblemDetailsService service) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken ct)
    {
        if (ex is ActiveCartValidationException or ActiveCartValidationException or PurchasedCartValidationException)
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
        else if (ex is Domain.Common.Exceptions.ActiveCarts.CartItems.ActiveCartItemNotFoundException)
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
        else if (ex is ActiveCartItemDeliveryException or ActiveCartAlreadyExistsException)
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
        else if (ex is PurchasedCartAuthorizationException)
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
        else if (ex is ActiveCartNotFoundException or ActiveCartItemNotFoundException or PurchasedCartNotFoundException or PurchasedCartItemNotFoundException)
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
