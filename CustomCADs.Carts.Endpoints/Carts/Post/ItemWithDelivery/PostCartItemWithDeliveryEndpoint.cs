using CustomCADs.Carts.Application.Carts.Commands.AddItemWithDelivery;
using CustomCADs.Carts.Application.Carts.Queries.GetItem;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Endpoints.Carts.Post.ItemWithDelivery;

public sealed class PostCartItemWithDeliveryEndpoint(IRequestSender sender)
    : Endpoint<PostCartItemWithDeliveryRequest, CartItemResponse>
{
    public override void Configure()
    {
        Post("item/delivery");
        Group<CartsGroup>();
        Description(d => d
            .WithSummary("03. Add Item (Delivery)")
            .WithDescription("Add an Item with Delivery to your Cart by specifying the Cart and Product's Ids, Quantity and if you want it delivered")
        );
    }

    public override async Task HandleAsync(PostCartItemWithDeliveryRequest req, CancellationToken ct)
    {
        CartId cartId = new(req.CartId);
        AccountId buyerId = User.GetAccountId();

        AddCartItemWithDeliveryCommand commnad = new(
            Id: cartId,
            Quantity: req.Quantity,
            Weight: req.Weight,
            ProductId: new ProductId(req.ProductId),
            BuyerId: buyerId
        );
        CartItemId itemId = await sender.SendCommandAsync(commnad, ct).ConfigureAwait(false);

        GetCartItemByIdQuery query = new(cartId, itemId, buyerId);
        CartItemDto item = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        var response = item.ToCartItemResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
