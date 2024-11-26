using CustomCADs.Gallery.Application.Carts.Commands.AddItem;
using CustomCADs.Shared.Core.Common.TypedIds.Gallery;

namespace CustomCADs.Gallery.Endpoints.Carts.Post.Item;

public class PostCartItemEndpoint(IRequestSender sender)
    : Endpoint<PostCartItemRequest, Guid>
{
    public override void Configure()
    {
        Post("items");
        Group<CartsGroup>();
        Description(d => d.WithSummary("2. I want to add an Item to my Cart."));
    }

    public override async Task HandleAsync(PostCartItemRequest req, CancellationToken ct)
    {
        AddCartItemCommand commnad = new(
            Id: new(req.CartId),
            DeliveryType: req.DeliveryType,
            Quantity: req.Quantity,
            ProductId: new(req.ProductId),
            BuyerId: User.GetAccountId()
        );
        CartItemId itemId = await sender.SendCommandAsync(commnad, ct).ConfigureAwait(false);

        await SendOkAsync(itemId.Value).ConfigureAwait(false);
    }
}
