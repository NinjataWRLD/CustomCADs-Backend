using CustomCADs.Carts.Application.Carts.Commands.AddItem;
using CustomCADs.Shared.Core.Common.TypedIds.Gallery;
using CustomCADs.Shared.Core.Common.TypedIds.Inventory;

namespace CustomCADs.Carts.Endpoints.Carts.Post.Items;

public sealed class PostCartItemEndpoint(IRequestSender sender)
    : Endpoint<PostCartItemRequest, Guid>
{
    public override void Configure()
    {
        Post("items");
        Group<CartsGroup>();
        Description(d => d
            .WithSummary("02. Add Item")
            .WithDescription("Add an Item to your Cart by specifying the Cart and Product's Ids and the Delivery Type and Quantity")
        );
    }

    public override async Task HandleAsync(PostCartItemRequest req, CancellationToken ct)
    {
        AddCartItemCommand commnad = new(
            Id: new CartId(req.CartId),
            DeliveryType: req.DeliveryType,
            Quantity: req.Quantity,
            ProductId: new ProductId(req.ProductId),
            BuyerId: User.GetAccountId()
        );
        CartItemId itemId = await sender.SendCommandAsync(commnad, ct).ConfigureAwait(false);

        await SendOkAsync(itemId.Value).ConfigureAwait(false);
    }
}
