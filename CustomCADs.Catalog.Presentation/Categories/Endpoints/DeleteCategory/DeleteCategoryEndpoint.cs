using CustomCADs.Catalog.Application.Categories.Commands.Delete;
using CustomCADs.Catalog.Application.Categories.Queries.ExistsById;
using FastEndpoints;
using MediatR;

namespace CustomCADs.Catalog.Presentation.Categories.Endpoints.DeleteCategory;

using static Helpers.ApiMessages;

public class DeleteCategoryEndpoint(IMediator mediator) : Endpoint<DeleteCategoryRequest>
{
    public override void Configure()
    {
        Delete("{id}");
        Group<CategoriesGroup>();
    }

    public override async Task HandleAsync(DeleteCategoryRequest req, CancellationToken ct)
    {
        CategoryExistsByIdQuery query = new(req.Id);
        bool exists = await mediator.Send(query, ct).ConfigureAwait(false);

        if (!exists)
        {
            ValidationFailures.Add(new()
            {
                ErrorMessage = NotFound,
            });
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        DeleteCategoryCommand command = new(req.Id);
        await mediator.Send(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
