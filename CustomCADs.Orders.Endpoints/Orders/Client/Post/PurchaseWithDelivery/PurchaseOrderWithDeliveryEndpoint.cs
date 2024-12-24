using CustomCADs.Orders.Application.Orders.Commands.PurchaseWithDelivery;

namespace CustomCADs.Orders.Endpoints.Orders.Client.Post.PurchaseWithDelivery;

public sealed class PurchaseOrderWithDeliveryEndpoint(IRequestSender sender)
    : Endpoint<PurchaseOrderWithDeliveryRequest, string>
{
    public override void Configure()
    {
        Post("purchase/delivery");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("11. Purchase (Delivery)")
            .WithDescription("Purchase the Order with Delivery")
        );
    }

    public override async Task HandleAsync(PurchaseOrderWithDeliveryRequest req, CancellationToken ct)
    {
        PurchaseOrderWithDeliveryCommand command = new(
            PaymentMethodId: req.PaymentMethodId,
            Weight: req.Weight,
            OrderId: new OrderId(req.OrderId),
            Address: req.Address,
            Contact: req.Contact,
            BuyerId: User.GetAccountId()
        ); 
        string message = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendOkAsync(message).ConfigureAwait(false);
    }
}
