using CustomCADs.Carts.Application.ActiveCarts.Commands.Item.Remove;
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
            .WithSummary("07. Remove Item")
            .WithDescription("Remove an Item from your Cart")
        );
    }

    public override async Task HandleAsync(DeleteActiveCartItemRequest req, CancellationToken ct)
    {
        RemoveActiveCartItemCommand commnad = new(
            ItemId: ActiveCartItemId.New(req.ItemId),
            BuyerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(commnad, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
