using CustomCADs.Carts.Application.Carts.Commands.AddItem;
using CustomCADs.Carts.Application.Carts.Queries.GetItem;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Endpoints.Carts.Post.Item;

public sealed class PostCartItemEndpoint(IRequestSender sender)
    : Endpoint<PostCartItemRequest, CartItemResponse>
{
    public override void Configure()
    {
        Post("item");
        Group<CartsGroup>();
        Description(d => d
            .WithSummary("02. Add Item")
            .WithDescription("Add an Item to your Cart by specifying the Cart and Product's Ids")
        );
    }

    public override async Task HandleAsync(PostCartItemRequest req, CancellationToken ct)
    {
        CartId cartId = new(req.CartId);
        AccountId buyerId = User.GetAccountId();

        AddCartItemCommand commnad = new(
            Id: cartId,
            ProductId: new ProductId(req.ProductId),
            Weight: req.Weight,
            BuyerId: buyerId
        );
        CartItemId itemId = await sender.SendCommandAsync(commnad, ct).ConfigureAwait(false);

        GetCartItemByIdQuery query = new(cartId, itemId, buyerId);
        CartItemDto item = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        var response = item.ToCartItemResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
