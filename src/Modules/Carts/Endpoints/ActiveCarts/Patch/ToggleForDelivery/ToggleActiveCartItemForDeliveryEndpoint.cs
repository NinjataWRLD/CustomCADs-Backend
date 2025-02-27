using CustomCADs.Carts.Application.ActiveCarts.Commands.Item.ToggleForDelivery;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Patch.ToggleForDelivery;

public class ToggleActiveCartItemForDeliveryEndpoint(IRequestSender sender)
    : Endpoint<ToggleActiveCartItemForDeliveryRequest>
{
    public override void Configure()
    {
        Patch("delivery");
        Group<ActiveCartsGroup>();
        Description(d => d
            .WithSummary("06. Toggle ForDelivery")
            .WithDescription("Turn on/off the Cart Item's planned Delivery")
        );
    }

    public override async Task HandleAsync(ToggleActiveCartItemForDeliveryRequest req, CancellationToken ct)
    {
        ToggleActiveCartItemForDeliveryCommand command = new(
            BuyerId: User.GetAccountId(),
            ProductId: ProductId.New(req.ProductId)
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
