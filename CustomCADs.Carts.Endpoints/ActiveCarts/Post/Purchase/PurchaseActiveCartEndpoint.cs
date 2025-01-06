using CustomCADs.Carts.Application.ActiveCarts.Commands.Purchase;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Post.Purchase;

public sealed class PurchaseActiveCartEndpoint(IRequestSender sender)
    : Endpoint<PurchaseActiveCartRequest, string>
{
    public override void Configure()
    {
        Post("purchase");
        Group<ActiveCartsGroup>();
        Description(d => d
            .WithSummary("09. Purchase")
            .WithDescription("Purchase all the Items in the Cart")
        );
    }

    public override async Task HandleAsync(PurchaseActiveCartRequest req, CancellationToken ct)
    {
        PurchaseActiveCartCommand command = new(
            PaymentMethodId: req.PaymentMethodId,
            BuyerId: User.GetAccountId()
        );
        string message = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendOkAsync(message).ConfigureAwait(false);
    }
}
