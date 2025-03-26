﻿using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Item.ToggleForDelivery;
using CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Patch.ToggleForDelivery;

public class ToggleActiveCartItemForDeliveryEndpoint(IRequestSender sender)
    : Endpoint<ToggleActiveCartItemForDeliveryRequest>
{
    public override void Configure()
    {
        Patch("delivery");
        Group<ActiveCartsGroup>();
        Description(d => d
            .WithSummary("Toggle ForDelivery")
            .WithDescription("Turn on/off the Cart Item's planned Delivery")
        );
    }

    public override async Task HandleAsync(ToggleActiveCartItemForDeliveryRequest req, CancellationToken ct)
    {
        ToggleActiveCartItemForDeliveryCommand command = new(
            BuyerId: User.GetAccountId(),
            ProductId: ProductId.New(req.ProductId),
            CustomizationId: CustomizationId.New(req.CustomizationId)
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
