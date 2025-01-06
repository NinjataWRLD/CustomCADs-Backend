using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Common.Exceptions.Products;
using Microsoft.AspNetCore.Diagnostics;

namespace CustomCADs.Catalog.Endpoints.Common;

using static StatusCodes;

public class CatalogExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken ct)
    {
        if (ex is ProductValidationException or ProductStatusException)
        {
            context.Response.StatusCode = Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Invalid Request Parameters",
                message = ex.Message,
            }, ct).ConfigureAwait(false);
        }
        else if (ex is ProductAuthorizationException)
        {
            context.Response.StatusCode = Status403Forbidden;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Authorization Issue",
                message = ex.Message
            }, ct).ConfigureAwait(false);
        }
        else if (ex is ProductNotFoundException)
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
