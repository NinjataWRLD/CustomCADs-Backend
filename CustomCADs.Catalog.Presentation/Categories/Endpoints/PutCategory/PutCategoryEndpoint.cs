using CustomCADs.Catalog.Application.Categories.Commands;
using CustomCADs.Catalog.Application.Categories.Commands.Edit;
using FastEndpoints;
using MediatR;

namespace CustomCADs.Catalog.Presentation.Categories.Endpoints.PutCategory;

public class PutCategoryEndpoint(IMediator mediator) : Endpoint<PutCategoryRequest>
{
    public override void Configure()
    {
        Put("{id}");
        Group<CategoriesGroup>();
    }

    public override async Task HandleAsync(PutCategoryRequest req, CancellationToken ct)
    {
        CategoryWriteDto category = new() { Name = req.Name };
        EditCategoryCommand command = new(req.Id, category);
        await mediator.Send(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
