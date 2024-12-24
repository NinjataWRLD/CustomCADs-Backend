using CustomCADs.Carts.Application.Carts.Commands.AddItem;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Endpoints.Carts.Post.Item;

public sealed class PostCartItemEndpoint(IRequestSender sender)
    : Endpoint<PostCartItemRequest, Guid>
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
        AddCartItemCommand commnad = new(
            Id: new CartId(req.CartId),
            ProductId: new ProductId(req.ProductId),
            Weight: req.Weight,
            BuyerId: User.GetAccountId()
        );
        CartItemId itemId = await sender.SendCommandAsync(commnad, ct).ConfigureAwait(false);

        await SendOkAsync(itemId.Value).ConfigureAwait(false);
    }
}
