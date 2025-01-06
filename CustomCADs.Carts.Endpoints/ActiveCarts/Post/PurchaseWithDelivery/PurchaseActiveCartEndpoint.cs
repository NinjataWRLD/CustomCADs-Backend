using CustomCADs.Carts.Application.ActiveCarts.Commands.PurchaseWithDelivery;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Post.PurchaseWithDelivery;

public sealed class PurchaseActiveCartEndpoint(IRequestSender sender)
    : Endpoint<PurchaseActiveCartRequest, string>
{
    public override void Configure()
    {
        Post("purchase/delivery");
        Group<ActiveCartsGroup>();
        Description(d => d
            .WithSummary("11. Purchase (Delivery)")
            .WithDescription("Purchase all the Items in the Cart with Delivery")
        );
    }

    public override async Task HandleAsync(PurchaseActiveCartRequest req, CancellationToken ct)
    {
        PurchaseActiveCartWithDeliveryCommand command = new(
            PaymentMethodId: req.PaymentMethodId,
            ShipmentService: req.ShipmentService,
            Address: req.Address,
            Contact: req.Contact,
            BuyerId: User.GetAccountId()
        );
        string message = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendOkAsync(message).ConfigureAwait(false);
    }
}
