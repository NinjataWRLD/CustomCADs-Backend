using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Common.Exceptions.Products;
using Microsoft.AspNetCore.Diagnostics;

namespace CustomCADs.Catalog.Endpoints.Common;

public class CatalogExceptionHandler(IProblemDetailsService service) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken ct)
        => ex switch
        {
            ProductValidationException or ProductStatusException
                => await service.BadRequestResponseAsync(context, ex).ConfigureAwait(false),

            ProductAuthorizationException
                => await service.ForbiddenResponseAsync(context, ex).ConfigureAwait(false),

            ProductNotFoundException
                => await service.NotFoundResponseAsync(context, ex).ConfigureAwait(false),

            _ => false
        };
}
