﻿using CustomCADs.Identity.Application.Common.Exceptions;
using CustomCADs.Identity.Domain.Common.Exceptions.Users;
using Microsoft.AspNetCore.Diagnostics;

namespace CustomCADs.Identity.Endpoints.Common;

using static StatusCodes;

public class IdentityExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken ct)
    {
        if (ex is UserValidationException or UserPasswordException)
        {
            context.Response.StatusCode = Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Invalid Request Parameters",
                message = ex.Message,
            }, ct).ConfigureAwait(false);
        }
        else if (ex is UserRegisterException or UserLoginException or UserRefreshTokenException)
        {
            context.Response.StatusCode = Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Inappropriately Unauthenticated",
                message = ex.Message,
            }, ct).ConfigureAwait(false);
        }
        else if (ex is UserLockedOutException)
        {
            context.Response.StatusCode = Status423Locked;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Account Locked",
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
        else if (ex is UserCreationException)
        {
            context.Response.StatusCode = Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Inside Error, contact Support",
                message = ex.Message,
            }, ct).ConfigureAwait(false);
        }

        return true;
    }
}