using CustomCADs.Carts.Application.ActiveCarts.Commands.AddItemWithDelivery;
using CustomCADs.Carts.Application.ActiveCarts.Queries.GetItem;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Post.ItemWithDelivery;

public sealed class PostActiveCartItemWithDeliveryEndpoint(IRequestSender sender)
    : Endpoint<PostActiveCartItemWithDeliveryRequest, ActiveCartItemResponse>
{
    public override void Configure()
    {
        Post("item/delivery");
        Group<ActiveCartsGroup>();
        Description(d => d
            .WithSummary("03. Add Item (Delivery)")
            .WithDescription("Add an Item with Delivery to your Cart by specifying the Cart and Product's Ids, Quantity and if you want it delivered")
        );
    }

    public override async Task HandleAsync(PostActiveCartItemWithDeliveryRequest req, CancellationToken ct)
    {
        AccountId buyerId = User.GetAccountId();

        AddActiveCartItemWithDeliveryCommand commnad = new(
            Weight: req.Weight,
            ProductId: new ProductId(req.ProductId),
            BuyerId: buyerId
        );
        ActiveCartItemId itemId = await sender.SendCommandAsync(commnad, ct).ConfigureAwait(false);

        GetActiveCartItemByIdQuery query = new(
            BuyerId: buyerId, 
            ItemId: itemId
        );
        ActiveCartItemDto item = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        var response = item.ToCartItemResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
