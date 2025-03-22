using CustomCADs.Accounts.Application.Common.Exceptions;
using CustomCADs.Accounts.Domain.Accounts.Exceptions;
using CustomCADs.Accounts.Domain.Roles.Exceptions;
using CustomCADs.Shared.API;
using Microsoft.AspNetCore.Diagnostics;

namespace CustomCADs.Accounts.Endpoints.Common;

public class AccountsExceptionHandler(IProblemDetailsService service) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken ct)
        => ex switch
        {
            AccountValidationException or RoleValidationException
                => await service.BadRequestResponseAsync(context, ex).ConfigureAwait(false),

            AccountNotFoundException or RoleNotFoundException
                => await service.NotFoundResponseAsync(context, ex).ConfigureAwait(false),

            _ => false
        };
}
