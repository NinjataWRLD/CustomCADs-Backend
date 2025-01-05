using CustomCADs.Carts.Application.ActiveCarts.Commands.RemoveItem;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Delete.Items;

public sealed class DeleteActiveCartItemEndpoint(IRequestSender sender)
    : Endpoint<DeleteActiveCartItemRequest>
{
    public override void Configure()
    {
        Delete("items");
        Group<ActiveCartsGroup>();
        Description(d => d
            .WithSummary("05. Remove Item")
            .WithDescription("Remove an Item from your Cart by specifying the Cart and Item's Ids")
        );
    }

    public override async Task HandleAsync(DeleteActiveCartItemRequest req, CancellationToken ct)
    {
        RemoveActiveCartItemCommand commnad = new(
            ItemId: new ActiveCartItemId(req.ItemId),
            BuyerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(commnad, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
