using CustomCADs.Customizations.Application.Common.Exceptions;
using CustomCADs.Customizations.Domain.Common.Exceptions.Customizations;
using CustomCADs.Customizations.Domain.Common.Exceptions.Materials;
using Microsoft.AspNetCore.Diagnostics;

namespace CustomCADs.Customizations.Endpoints.Common;

public class CustomizationsExceptionHandler(IProblemDetailsService service) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken ct)
        => ex switch
        {
            MaterialValidationException or CustomizationValidationException
                => await service.BadRequestResponseAsync(context, ex).ConfigureAwait(false),

            MaterialNotFoundException or CustomizationNotFoundException
                => await service.NotFoundResponseAsync(context, ex).ConfigureAwait(false),

            _ => false
        };
}
