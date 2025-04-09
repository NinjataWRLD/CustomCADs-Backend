using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Post.PurchaseWithDelivery;

public sealed class PurchaseActiveCartEndpoint(IRequestSender sender)
    : Endpoint<PurchaseActiveCartRequest, string>
{
    public override void Configure()
    {
        Post("purchase-delivery");
        Group<ActiveCartsGroup>();
        Description(d => d
            .WithSummary("Purchase (Delivery)")
            .WithDescription("Purchase all the Items in the Cart (and Ship those marked for Delivery)")
        );
    }

    public override async Task HandleAsync(PurchaseActiveCartRequest req, CancellationToken ct)
    {
        string message = await sender.SendCommandAsync(
            new PurchaseActiveCartWithDeliveryCommand(
                PaymentMethodId: req.PaymentMethodId,
                ShipmentService: req.ShipmentService,
                Address: req.Address,
                Contact: req.Contact,
                BuyerId: User.GetAccountId()
            ),
            ct
        ).ConfigureAwait(false);

        await SendOkAsync(message).ConfigureAwait(false);
    }
}
