using CustomCADs.Categories.Application.Common.Exceptions;
using CustomCADs.Categories.Domain.Common.Exceptions.Categories;
using CustomCADs.Shared.API;
using Microsoft.AspNetCore.Diagnostics;

namespace CustomCADs.Categories.Endpoints.Common;

public class CategoriesExceptionHandler(IProblemDetailsService service) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken ct)
        => ex switch
        {
            CategoryValidationException
                => await service.BadRequestResponseAsync(context, ex).ConfigureAwait(false),

            CategoryNotFoundException
                => await service.NotFoundResponseAsync(context, ex).ConfigureAwait(false),

            _ => false
        };
}
