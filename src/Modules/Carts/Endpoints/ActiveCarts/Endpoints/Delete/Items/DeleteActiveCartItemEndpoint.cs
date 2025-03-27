using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Item.Remove;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Delete.Items;

public sealed class DeleteActiveCartItemEndpoint(IRequestSender sender)
    : Endpoint<DeleteActiveCartItemRequest>
{
    public override void Configure()
    {
        Delete("items");
        Group<ActiveCartsGroup>();
        Description(d => d
            .WithSummary("Remove Item")
            .WithDescription("Remove an Item from your Cart")
        );
    }

    public override async Task HandleAsync(DeleteActiveCartItemRequest req, CancellationToken ct)
    {
        RemoveActiveCartItemCommand commnad = new(
            ProductId: ProductId.New(req.ProductId),
            BuyerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(commnad, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
