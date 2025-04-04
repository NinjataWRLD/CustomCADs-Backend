﻿using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Add;
using CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.GetSingle;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Post.Item;

public sealed class PostActiveCartItemEndpoint(IRequestSender sender)
    : Endpoint<PostActiveCartItemRequest, ActiveCartItemResponse>
{
    public override void Configure()
    {
        Post("");
        Group<ActiveCartsGroup>();
        Description(d => d
            .WithSummary("Add Item")
            .WithDescription("Add an Item to your Cart")
        );
    }

    public override async Task HandleAsync(PostActiveCartItemRequest req, CancellationToken ct)
    {
        AccountId buyerId = User.GetAccountId();

        AddActiveCartItemCommand command = new(
            ProductId: ProductId.New(req.ProductId),
            ForDelivery: req.ForDelivery,
            CustomizationId: CustomizationId.New(req.CustomizationId),
            BuyerId: buyerId
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        GetActiveCartItemQuery query = new(
            BuyerId: buyerId,
            ProductId: ProductId.New(req.ProductId)
        );
        ActiveCartItemDto item = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        ActiveCartItemResponse response = item.ToResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
