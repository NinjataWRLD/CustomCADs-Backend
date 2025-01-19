using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Files.Application.Common.Exceptions;
using CustomCADs.Files.Domain.Common.Exceptions.Cads;
using CustomCADs.Files.Domain.Common.Exceptions.Images;
using CustomCADs.Shared.API;
using CustomCADs.Shared.Abstractions.Payment.Exceptions;
using CustomCADs.Shared.Core.Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace CustomCADs.Presentation;

using static StatusCodes;

public class GlobalExceptionHandler(IProblemDetailsService service) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken ct)
        => ex switch
        {
            CadValidationException or ImageValidationException
                => await service.BadRequestResponseAsync(context, ex).ConfigureAwait(false),

            ValidationException
                => await service.BadRequestResponseAsync(context, ex, "Validation Error").ConfigureAwait(false),

            PaymentFailedException
                => await service.BadRequestResponseAsync(context, ex, "Payment Failure").ConfigureAwait(false),
            
            DatabaseException
                => await service.BadRequestResponseAsync(context, ex, "Database Error").ConfigureAwait(false),

            ProductAuthorizationException
                => await service.ForbidednResponseAsync(context, ex).ConfigureAwait(false),

            CadNotFoundException or ImageNotFoundException
                => await service.NotFoundResponseAsync(context, ex).ConfigureAwait(false),

            DatabaseConflictException
                => await service.CusotmResponseAsync(context, ex, Status409Conflict, "Database Conflict").ConfigureAwait(false),

            _ => await service.InternalServerErrorResponseAsync(context, ex).ConfigureAwait(false),
        };
}
