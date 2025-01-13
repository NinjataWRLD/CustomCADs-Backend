using CustomCADs.Carts.Application.ActiveCarts.Commands.Item.Add;
using CustomCADs.Carts.Application.ActiveCarts.Queries.GetItem;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

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
            .WithDescription("Add an Item to your Cart by specifying the Cart's Id, the Product's Id, the Weight and whether it's to be delivered")
        );
    }

    public override async Task HandleAsync(PostActiveCartItemRequest req, CancellationToken ct)
    {
        AccountId buyerId = User.GetAccountId();

        AddActiveCartItemCommand command = new(
            ProductId: ProductId.New(req.ProductId),
            Weight: req.Weight,
            ForDelivery: req.ForDelivery,
            BuyerId: buyerId
        );
        ActiveCartItemId itemId = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        GetActiveCartItemByIdQuery query = new(
            BuyerId: buyerId, 
            ItemId: itemId
        );
        ActiveCartItemDto item = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        ActiveCartItemResponse response = item.ToCartItemResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
