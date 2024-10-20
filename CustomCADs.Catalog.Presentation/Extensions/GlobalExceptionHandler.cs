using CustomCADs.Catalog.Application.Categories.Common;
using CustomCADs.Catalog.Application.Products.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Catalog.Presentation.Extensions;

using static StatusCodes;

public class GlobalExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken cancellationToken)
    {
        if (ex is ProductNotFoundException or CategoryNotFoundException)
        {
            context.Response.StatusCode = Status404NotFound;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Resource Not Found",
                message = ex.Message
            }).ConfigureAwait(false);
        }
        else if (ex is DbUpdateConcurrencyException)
        {
            context.Response.StatusCode = Status409Conflict;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Database Conflict Ocurred",
                message = ex.Message
            }).ConfigureAwait(false);
        }
        else if (ex is DbUpdateException)
        {
            context.Response.StatusCode = Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Database Error",
                message = ex.Message
            }).ConfigureAwait(false);
        }
        else
        {
            context.Response.StatusCode = Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Internal Server Error",
                message = ex.Message
            }).ConfigureAwait(false);
        }

        return true;
    }
}
