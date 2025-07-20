using CustomCADs.Shared.Abstractions.Payment.Exceptions;
using CustomCADs.Shared.API;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.Shared.Core.Common.Exceptions.Persistence;
using Microsoft.AspNetCore.Diagnostics;

namespace CustomCADs.Presentation;

using static StatusCodes;

public class GlobalExceptionHandler(IProblemDetailsService service) : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken ct)
		=> ex switch
		{
			_ when ex is FluentValidation.ValidationException || ex.IsType(typeof(CustomValidationException<>))
				=> await service.BadRequestResponseAsync(context, ex, "Validation Error").ConfigureAwait(false),

			PaymentFailedException pfex
				=> await service.PaymentFailedResponseAsync(context, ex, pfex.ClientSecret).ConfigureAwait(false),

			DatabaseException
				=> await service.BadRequestResponseAsync(context, ex, "Database Error").ConfigureAwait(false),

			_ when ex.IsType(typeof(CustomNotFoundException<>))
				 => await service.NotFoundResponseAsync(context, ex).ConfigureAwait(false),

			_ when ex.IsType(typeof(CustomAuthorizationException<>))
				 => await service.ForbiddenResponseAsync(context, ex).ConfigureAwait(false),

			DatabaseConflictException
				=> await service.CustomResponseAsync(context, ex, Status409Conflict, "Database Conflict").ConfigureAwait(false),

			_ => await service.InternalServerErrorResponseAsync(context, ex).ConfigureAwait(false),
		};
}
