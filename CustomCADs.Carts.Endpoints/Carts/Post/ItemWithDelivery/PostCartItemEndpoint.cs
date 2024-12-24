using CustomCADs.Carts.Application.Carts.Commands.AddItemWithDelivery;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Endpoints.Carts.Post.ItemWithDelivery;

public sealed class PostCartItemEndpoint(IRequestSender sender)
    : Endpoint<PostCartItemRequest, Guid>
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

    public override async Task HandleAsync(PostCartItemRequest req, CancellationToken ct)
    {
        AddCartItemWithDeliveryCommand commnad = new(
            Id: new CartId(req.CartId),
            Quantity: req.Quantity,
            Weight: req.Weight,
            ProductId: new ProductId(req.ProductId),
            BuyerId: User.GetAccountId()
        );
        CartItemId itemId = await sender.SendCommandAsync(commnad, ct).ConfigureAwait(false);

        await SendOkAsync(itemId.Value).ConfigureAwait(false);
    }
}
