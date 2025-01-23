using CustomCADs.Identity.Application.Common.Exceptions;
using CustomCADs.Identity.Domain.Common.Exceptions.Users;
using Microsoft.AspNetCore.Diagnostics;

namespace CustomCADs.Identity.Endpoints.Common;

using static StatusCodes;

public class IdentityExceptionHandler(IProblemDetailsService service) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken ct)
        => ex switch
        {
            UserValidationException or UserPasswordException
                => await service.BadRequestResponseAsync(context, ex).ConfigureAwait(false),

            UserNotFoundException or RoleNotFoundException
                => await service.NotFoundResponseAsync(context, ex).ConfigureAwait(false),

            UserCreationException
                => await service.InternalServerErrorResponseAsync(context, ex).ConfigureAwait(false),

            UserRegisterException or UserLoginException or UserRefreshTokenException
                => await service.UnauthorizedResponseAsync(context, ex).ConfigureAwait(false),

            UserLockedOutException
                => await service.CusotmResponseAsync(context, ex, Status423Locked, "Account Locked").ConfigureAwait(false),

            _ => false
        };
}