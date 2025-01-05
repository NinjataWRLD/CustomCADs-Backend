using CustomCADs.Carts.Application.ActiveCarts.Commands.AddItem;
using CustomCADs.Carts.Application.ActiveCarts.Queries.GetItem;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using System;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Post.Item;

public sealed class PostActiveCartItemEndpoint(IRequestSender sender)
    : Endpoint<PostActiveCartItemRequest, ActiveCartItemResponse>
{
    public override void Configure()
    {
        Post("item");
        Group<ActiveCartsGroup>();
        Description(d => d
            .WithSummary("02. Add Item")
            .WithDescription("Add an Item to your Cart by specifying the Cart and Product's Ids")
        );
    }

    public override async Task HandleAsync(PostActiveCartItemRequest req, CancellationToken ct)
    {
        AccountId buyerId = User.GetAccountId();

        AddActiveCartItemCommand commnad = new(
            ProductId: new ProductId(req.ProductId),
            Weight: req.Weight,
            BuyerId: buyerId
        );
        ActiveCartItemId itemId = await sender.SendCommandAsync(commnad, ct).ConfigureAwait(false);

        GetActiveCartItemByIdQuery query = new(
            BuyerId: buyerId, 
            ItemId: itemId
        );
        ActiveCartItemDto item = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        ActiveCartItemResponse response = item.ToCartItemResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
