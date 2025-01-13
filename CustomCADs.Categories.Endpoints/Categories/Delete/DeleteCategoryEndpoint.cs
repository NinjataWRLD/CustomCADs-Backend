using CustomCADs.Categories.Application.Categories.Commands.Delete;

namespace CustomCADs.Categories.Endpoints.Categories.Delete;

public sealed class DeleteCategoryEndpoint(IRequestSender sender)
    : Endpoint<DeleteCategoryRequest>
{
    public override void Configure()
    {
        Delete("");
        Group<CategoriesGroup>();
        Description(d => d
            .WithSummary("5. Delete")
            .WithDescription("Delete a Category")
        );
    }

    public override async Task HandleAsync(DeleteCategoryRequest req, CancellationToken ct)
    {
        DeleteCategoryCommand command = new(
            Id: CategoryId.New(req.Id)
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
