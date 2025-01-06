using CustomCADs.Categories.Application.Common.Exceptions;
using CustomCADs.Categories.Domain.Common.Exceptions.Categories;
using Microsoft.AspNetCore.Diagnostics;

namespace CustomCADs.Categories.Endpoints.Common;

using static StatusCodes;

public class CategoriesExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken ct)
    {
        if (ex is CategoryValidationException)
        {
            context.Response.StatusCode = Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Invalid Request Parameters",
                message = ex.Message,
            }, ct).ConfigureAwait(false);
        }
        else if (ex is CategoryNotFoundException)
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
