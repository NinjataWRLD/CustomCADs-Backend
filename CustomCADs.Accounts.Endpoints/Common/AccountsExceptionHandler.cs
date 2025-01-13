using CustomCADs.Accounts.Application.Common.Exceptions;
using CustomCADs.Accounts.Domain.Common.Exceptions.Accounts;
using CustomCADs.Accounts.Domain.Common.Exceptions.Roles;
using Microsoft.AspNetCore.Diagnostics;

namespace CustomCADs.Accounts.Endpoints.Common;

using static StatusCodes;

public class AccountsExceptionHandler(IProblemDetailsService service) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken ct)
    {
        if (ex is AccountValidationException or RoleValidationException)
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
        else if (ex is AccountNotFoundException or RoleNotFoundException)
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
                }
            }).ConfigureAwait(false);
        }

        return true;
    }
}
