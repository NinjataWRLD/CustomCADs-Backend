﻿using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Purchase.Normal;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Post.Purchase;

public sealed class PurchaseActiveCartEndpoint(IRequestSender sender)
    : Endpoint<PurchaseActiveCartRequest, string>
{
    public override void Configure()
    {
        Post("purchase");
        Group<ActiveCartsGroup>();
        Description(d => d
            .WithSummary("Purchase")
            .WithDescription("Purchase all the Items in the Cart")
        );
    }

    public override async Task HandleAsync(PurchaseActiveCartRequest req, CancellationToken ct)
    {
        string message = await sender.SendCommandAsync(
            new PurchaseActiveCartCommand(
                PaymentMethodId: req.PaymentMethodId,
                BuyerId: User.GetAccountId()
            ),
            ct
        ).ConfigureAwait(false);

        await SendOkAsync(message).ConfigureAwait(false);
    }
}
