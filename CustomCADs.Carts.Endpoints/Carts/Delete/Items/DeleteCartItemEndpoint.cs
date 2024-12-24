using CustomCADs.Carts.Application.Carts.Commands.RemoveItem;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;

namespace CustomCADs.Carts.Endpoints.Carts.Delete.Items;

public sealed class DeleteCartItemEndpoint(IRequestSender sender)
    : Endpoint<DeleteItemItemRequest>
{
    public override void Configure()
    {
        Delete("items");
        Group<CartsGroup>();
        Description(d => d
            .WithSummary("07. Remove Item")
            .WithDescription("Remove an Item from your Cart by specifying the Cart and Item's Ids")
        );
    }

    public override async Task HandleAsync(DeleteItemItemRequest req, CancellationToken ct)
    {
        RemoveCartItemCommand commnad = new(
            Id: new CartId(req.CartId),
            ItemId: new CartItemId(req.ItemId),
            BuyerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(commnad, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
