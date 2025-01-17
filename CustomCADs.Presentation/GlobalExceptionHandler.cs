using CustomCADs.Files.Application.Common.Exceptions;
using CustomCADs.Files.Domain.Common.Exceptions.Cads;
using CustomCADs.Files.Domain.Common.Exceptions.Images;
using CustomCADs.Shared.Application.Payment.Exceptions;
using CustomCADs.Shared.Core.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace CustomCADs.Presentation;

using static StatusCodes;

public class GlobalExceptionHandler(IProblemDetailsService service) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken ct)
    {
        if (ex is CadValidationException or ImageValidationException)
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
        else if (ex is CadNotFoundException or ImageNotFoundException)
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
        else if (ex is PaymentFailedException)
        {
            return await service.TryWriteAsync(new()
            {
                HttpContext = context,
                Exception = ex,
                ProblemDetails = new()
                {
                    Type = ex.GetType().Name,
                    Title = "Payment Failure",
                    Detail = ex.Message,
                    Status = Status400BadRequest,
                },
            }).ConfigureAwait(false);
        }
        else if (ex is DatabaseConflictException)
        {
            return await service.TryWriteAsync(new()
            {
                HttpContext = context,
                Exception = ex,
                ProblemDetails = new()
                {
                    Type = ex.GetType().Name,
                    Title = "Database Conflict",
                    Detail = ex.Message,
                    Status = Status409Conflict,
                },
            }).ConfigureAwait(false);
        }
        else if (ex is DatabaseException)
        {
            return await service.TryWriteAsync(new()
            {
                HttpContext = context,
                Exception = ex,
                ProblemDetails = new()
                {
                    Type = ex.GetType().Name,
                    Title = "Database Error",
                    Detail = ex.Message,
                    Status = Status400BadRequest,
                },
            }).ConfigureAwait(false);
        }
        else
        {
            return await service.TryWriteAsync(new()
            {
                HttpContext = context,
                Exception = ex,
                ProblemDetails = new()
                {
                    Type = ex.GetType().Name,
                    Title = "Internal Server Error",
                    Detail = ex.Message,
                    Status = Status500InternalServerError,
                },
            }).ConfigureAwait(false);
        }
    }
}
