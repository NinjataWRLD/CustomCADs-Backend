using CustomCADs.Carts.Application.ActiveCarts.Commands.SetActiveCartItemDelivery;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Patch.SetDelivery;

public class SetActiveCartItemForDeliveryEndpoint(IRequestSender sender)
    : Endpoint<SetActiveCartItemForDeliveryRequest>
{
    public override void Configure()
    {
        Patch("delivery");
        Group<ActiveCartsGroup>();
        Description(d => d
            .WithSummary("06. Set ForDelivery")
            .WithDescription("Turn on/off the Cart Item's planned Delivery")
        );
    }

    public override async Task HandleAsync(SetActiveCartItemForDeliveryRequest req, CancellationToken ct)
    {
        SetActiveCartItemForDeliveryCommand command = new(
            BuyerId: User.GetAccountId(),
            ItemId: new ActiveCartItemId(req.ItemId),
            Value: req.Value
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);
        
        await SendNoContentAsync().ConfigureAwait(false);
    }
}
