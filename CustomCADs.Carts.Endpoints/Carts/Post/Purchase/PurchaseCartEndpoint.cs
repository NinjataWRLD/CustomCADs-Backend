using CustomCADs.Carts.Application.Carts.Commands.Purchase;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;

namespace CustomCADs.Carts.Endpoints.Carts.Post.Purchase;

public sealed class PurchaseCartEndpoint(IRequestSender sender)
    : Endpoint<PurchaseCartRequest, string>
{
    public override void Configure()
    {
        Post("purchase");
        Group<CartsGroup>();
        Description(d => d
            .WithSummary("09. Purchase")
            .WithDescription("Purchase all the Items in the Cart")
        );
    }

    public override async Task HandleAsync(PurchaseCartRequest req, CancellationToken ct)
    {
        PurchaseCartCommand command = new(
            PaymentMethodId: req.PaymentMethodId,
            CartId: new CartId(req.CartId),
            BuyerId: User.GetAccountId()
        );
        string message = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendOkAsync(message).ConfigureAwait(false);
    }
}
