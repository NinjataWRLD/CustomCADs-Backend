using CustomCADs.Catalog.Application.Products.Commands.Internal.Delete;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Delete;

public sealed class DeleteProductEndpoint(IRequestSender sender)
    : Endpoint<DeleteProductRequest>
{
    public override void Configure()
    {
        Delete("");
        Group<CreatorGroup>();
        Description(d => d
            .WithSummary("Delete")
            .WithDescription("Delete your Product")
        );
    }

    public override async Task HandleAsync(DeleteProductRequest req, CancellationToken ct)
    {
        DeleteProductCommand command = new(
            Id: ProductId.New(req.Id),
            CreatorId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
