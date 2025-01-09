using CustomCADs.Carts.Application.ActiveCarts.Commands.Item.Quantity.Decrement;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Patch.DecrementQuantity;

public class DecreaeseActiveCartItemQuantityEndpoint(IRequestSender sender)
    : Endpoint<DecreaseActiveCartItemQuantityRequest>
{
    public override void Configure()
    {
        Patch("decrease");
        Group<ActiveCartsGroup>();
        Description(d => d
            .WithSummary("05. Decrease")
            .WithDescription("Decrease the Cart Item's quantity")
        );
    }

    public override async Task HandleAsync(DecreaseActiveCartItemQuantityRequest req, CancellationToken ct)
    {
        DecreaseActiveCartItemQuantityCommand command = new(
            BuyerId: User.GetAccountId(),
            ItemId: ActiveCartItemId.New(req.ItemId),
            Amount: req.Amount
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
