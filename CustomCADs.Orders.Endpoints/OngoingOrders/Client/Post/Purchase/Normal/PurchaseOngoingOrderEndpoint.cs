using CustomCADs.Orders.Application.OngoingOrders.Commands.Purchase.Normal;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Client.Post.Purchase.Normal;

public sealed class PurchaseOngoingOrderEndpoint(IRequestSender sender)
    : Endpoint<PurchaseOngoingOrderRequest, string>
{
    public override void Configure()
    {
        Post("purchase");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("09. Purchase")
            .WithDescription("Purchase the Order")
        );
    }

    public override async Task HandleAsync(PurchaseOngoingOrderRequest req, CancellationToken ct)
    {
        PurchaseOngoingOrderCommand command = new(
            OrderId: OngoingOrderId.New(req.Id),
            PaymentMethodId: req.PaymentMethodId,
            BuyerId: User.GetAccountId()
        );
        string message = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendOkAsync(message).ConfigureAwait(false);
    }
}
