using CustomCADs.Catalog.Application.Products.Commands.Delete;

namespace CustomCADs.Catalog.Endpoints.Products.Creator.Delete;

public sealed class DeleteProductEndpoint(IRequestSender sender)
    : Endpoint<DeleteProductRequest>
{
    public override void Configure()
    {
        Delete("");
        Group<ProductsGroup>();
        Description(d => d
            .WithSummary("09. Delete")
            .WithDescription("Delete your Product by specifying its Id")
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
