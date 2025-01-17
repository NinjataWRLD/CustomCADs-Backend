using CustomCADs.Identity.Application.Common.Exceptions;
using CustomCADs.Identity.Domain.Common.Exceptions.Users;
using Microsoft.AspNetCore.Diagnostics;

namespace CustomCADs.Identity.Endpoints.Common;

using static StatusCodes;

public class IdentityExceptionHandler(IProblemDetailsService service) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken ct)
    {
        if (ex is UserValidationException or UserPasswordException)
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
        else if (ex is UserRegisterException or UserLoginException or UserRefreshTokenException)
        {
            return await service.TryWriteAsync(new()
            {
                HttpContext = context,
                Exception = ex,
                ProblemDetails = new()
                {
                    Type = ex.GetType().Name,
                    Title = "Inappropriately Unauthenticated",
                    Detail = ex.Message,
                    Status = Status401Unauthorized,
                },
            }).ConfigureAwait(false);
        }
        else if (ex is UserLockedOutException)
        {
            return await service.TryWriteAsync(new()
            {
                HttpContext = context,
                Exception = ex,
                ProblemDetails = new()
                {
                    Type = ex.GetType().Name,
                    Title = "Account Locked",
                    Detail = ex.Message,
                    Status = Status423Locked,
                },
            }).ConfigureAwait(false);
        }
        else if (ex is UserNotFoundException or RoleNotFoundException)
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
        else if (ex is UserCreationException)
        {
            return await service.TryWriteAsync(new()
            {
                HttpContext = context,
                Exception = ex,
                ProblemDetails = new()
                {
                    Type = ex.GetType().Name,
                    Title = "Internal Error, contact Support",
                    Detail = ex.Message,
                    Status = Status500InternalServerError,
                },
            }).ConfigureAwait(false);
        }

        return false;
    }
}