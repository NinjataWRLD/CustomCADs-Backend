using CustomCADs.Categories.Application.Categories.Commands.Delete;
using CustomCADs.Categories.Application.Categories.Queries.ExistsById;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Categories.Endpoints.Categories.Delete;

using static ApiMessages;

public class DeleteCategoryEndpoint(IRequestSender sender)
    : Endpoint<DeleteCategoryRequest>
{
    public override void Configure()
    {
        Delete("{id}");
        Group<CategoriesGroup>();
        Description(d => d.WithSummary("5. I want to delete a Category"));
    }

    public override async Task HandleAsync(DeleteCategoryRequest req, CancellationToken ct)
    {
        CategoryId id = new(req.Id);
        CategoryExistsByIdQuery query = new(id);
        bool exists = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        if (!exists)
        {
            ValidationFailures.Add(new("Id", CategoryNotFound, req.Id));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        DeleteCategoryCommand command = new(id);
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
