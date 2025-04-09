using CustomCADs.Categories.Application.Categories.Commands.Internal.Delete;

namespace CustomCADs.Categories.Endpoints.Categories.Endpoints.Delete;

public sealed class DeleteCategoryEndpoint(IRequestSender sender)
    : Endpoint<DeleteCategoryRequest>
{
    public override void Configure()
    {
        Delete("");
        Group<CategoriesGroup>();
        Description(d => d
            .WithSummary("Delete")
            .WithDescription("Delete a Category")
        );
    }

    public override async Task HandleAsync(DeleteCategoryRequest req, CancellationToken ct)
    {
        await sender.SendCommandAsync(
            new DeleteCategoryCommand(
                Id: CategoryId.New(req.Id)
            ),
            ct
        ).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
