using CustomCADs.Carts.Application.Carts.Commands.PurchaseWithDelivery;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;

namespace CustomCADs.Carts.Endpoints.Carts.Post.PurchaseWithDelivery;

public sealed class PurchaseCartEndpoint(IRequestSender sender)
    : Endpoint<PurchaseCartRequest, string>
{
    public override void Configure()
    {
        Post("purchase/delivery");
        Group<CartsGroup>();
        Description(d => d
            .WithSummary("11. Purchase (Delivery)")
            .WithDescription("Purchase all the Items in the Cart with Delivery")
        );
    }

    public override async Task HandleAsync(PurchaseCartRequest req, CancellationToken ct)
    {
        PurchaseCartWithDeliveryCommand command = new(
            PaymentMethodId: req.PaymentMethodId,
            CartId: new CartId(req.CartId),
            Address: req.Address,
            Contact: req.Contact,
            BuyerId: User.GetAccountId()
        );
        string message = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendOkAsync(message).ConfigureAwait(false);
    }
}
