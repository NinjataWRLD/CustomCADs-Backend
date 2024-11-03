using CustomCADs.Account.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Account.Endpoints.Helpers;

using static StatusCodes;

public static class GlobalExceptionHandler
{
    public static async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken ct)
    {
        if (ex is UserNotFoundException or RoleNotFoundException)
        {
            context.Response.StatusCode = Status404NotFound;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Resource Not Found",
                message = ex.Message,
            }, ct).ConfigureAwait(false);
        }
        else if (ex is DbUpdateConcurrencyException)
        {
            context.Response.StatusCode = Status409Conflict;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Database Conflict Ocurred",
                message = ex.Message,
            }, ct).ConfigureAwait(false);
        }
        else if (ex is DbUpdateException)
        {
            context.Response.StatusCode = Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Database Error",
                message = ex.Message,
            }, ct).ConfigureAwait(false);
        }
        else
        {
            context.Response.StatusCode = Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Internal Server Error",
                message = ex.Message,
            }, ct).ConfigureAwait(false);
        }

        return true;
    }
}
