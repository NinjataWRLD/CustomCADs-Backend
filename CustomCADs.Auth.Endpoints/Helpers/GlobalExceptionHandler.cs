using CustomCADs.Auth.Application.Common.Exceptions.Roles;
using CustomCADs.Auth.Application.Common.Exceptions.Users;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Auth.Endpoints.Helpers;

using static StatusCodes;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken ct)
    {
        if (ex is UserValidationException)
        {
            context.Response.StatusCode = Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Invalid Request Parameters",
                message = ex.Message,
            }, ct).ConfigureAwait(false);
        }
        else if (ex is UserNotFoundException or RoleNotFoundException)
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