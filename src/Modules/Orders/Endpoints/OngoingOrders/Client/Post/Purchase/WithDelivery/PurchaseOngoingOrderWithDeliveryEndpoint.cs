using CustomCADs.Orders.Application.OngoingOrders.Commands.Purchase.WithDelivery;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Client.Post.Purchase.WithDelivery;

public sealed class PurchaseOngoingOrderWithDeliveryEndpoint(IRequestSender sender)
    : Endpoint<PurchaseOngoingOrderWithDeliveryRequest, string>
{
    public override void Configure()
    {
        Post("purchase-delivery");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("11. Purchase (Delivery)")
            .WithDescription("Purchase the Order with Delivery")
        );
    }

    public override async Task HandleAsync(PurchaseOngoingOrderWithDeliveryRequest req, CancellationToken ct)
    {
        PurchaseOngoingOrderWithDeliveryCommand command = new(
            OrderId: OngoingOrderId.New(req.Id),
            PaymentMethodId: req.PaymentMethodId,
            ShipmentService: req.ShipmentService,
            Count: req.Count,
            Address: req.Address,
            Contact: req.Contact,
            BuyerId: User.GetAccountId(),
            CustomizationId: CustomizationId.New(req.CustomizationId)
        );
        string message = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendOkAsync(message).ConfigureAwait(false);
    }
}
