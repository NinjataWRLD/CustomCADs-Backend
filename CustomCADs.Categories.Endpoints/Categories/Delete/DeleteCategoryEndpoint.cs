using CustomCADs.Categories.Application.Categories.Commands.Delete;

namespace CustomCADs.Categories.Endpoints.Categories.Delete;

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
        DeleteCategoryCommand command = new(Id: new(req.Id));
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
