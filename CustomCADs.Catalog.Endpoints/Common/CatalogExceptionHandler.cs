using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Common.Exceptions.Products;
using Microsoft.AspNetCore.Diagnostics;

namespace CustomCADs.Catalog.Endpoints.Common;

using static StatusCodes;

public class CatalogExceptionHandler(IProblemDetailsService service) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken ct)
    {
        if (ex is ProductValidationException or ProductStatusException)
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
        else if (ex is ProductAuthorizationException)
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
        else if (ex is ProductNotFoundException)
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
