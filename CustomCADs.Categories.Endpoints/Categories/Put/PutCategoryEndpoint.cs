using CustomCADs.Categories.Application.Categories.Commands;
using CustomCADs.Categories.Application.Categories.Commands.Edit;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Categories.Endpoints.Categories.Put;

public class PutCategoryEndpoint(IRequestSender sender)
    : Endpoint<PutCategoryRequest>
{
    public override void Configure()
    {
        Put("{id}");
        Group<CategoriesGroup>();
        Description(d => d.WithSummary("4. I want to edit a Category"));
    }

    public override async Task HandleAsync(PutCategoryRequest req, CancellationToken ct)
    {
        CategoryId id = new(req.Id);
        CategoryWriteDto category = new(req.Name);

        EditCategoryCommand command = new(id, category);
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
