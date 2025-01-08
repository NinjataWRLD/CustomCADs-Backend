using CustomCADs.Orders.Application.Orders.Commands.Purchase;

namespace CustomCADs.Orders.Endpoints.Orders.Client.Post.Purchase;

public sealed class PurchaseOrderEndpoint(IRequestSender sender)
    : Endpoint<PurchaseOrderRequest, string>
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

    public override async Task HandleAsync(PurchaseOrderRequest req, CancellationToken ct)
    {
        PurchaseOrderCommand command = new(
            PaymentMethodId: req.PaymentMethodId,
            OrderId: OrderId.New(req.OrderId),
            BuyerId: User.GetAccountId()
        );
        string message = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendOkAsync(message).ConfigureAwait(false);
    }
}
