using CustomCADs.Carts.Application.ActiveCarts.Commands.Item.Quantity.Increment;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Patch.IncrementQuantity;

public class IncreaseActiveCartItemQuantityEndpoint(IRequestSender sender)
    : Endpoint<IncreaseActiveCartItemQuantityRequest>
{
    public override void Configure()
    {
        Patch("increase");
        Group<ActiveCartsGroup>();
        Description(d => d
            .WithSummary("04. Increase")
            .WithDescription("Increase the Cart Item's quantity")
        );
    }

    public override async Task HandleAsync(IncreaseActiveCartItemQuantityRequest req, CancellationToken ct)
    {
        IncreaseActiveCartItemQuantityCommand command = new(
            BuyerId: User.GetAccountId(),
            ItemId: ActiveCartItemId.New(req.ItemId),
            Amount: req.Amount
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
