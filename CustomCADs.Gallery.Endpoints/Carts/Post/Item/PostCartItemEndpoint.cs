using CustomCADs.Gallery.Application.Carts.Commands.AddItem;
using CustomCADs.Gallery.Application.Carts.Queries.IsBuyer;
using CustomCADs.Gallery.Endpoints.Helpers;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Gallery;
using CustomCADs.Shared.Queries.Products;

namespace CustomCADs.Gallery.Endpoints.Carts.Post.Item;

using static ApiMessages;

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
        CartId id = new(req.CartId);
        IsCartBuyerQuery query = new(id, User.GetAccountId());

        bool userIsBuyer = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);
        if (!userIsBuyer)
        {
            ValidationFailures.Add(new("Id", ForbiddenAccess, id));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        GetProductPriceByIdQuery productQuery = new(new(req.ProductId));
        decimal price = await sender.SendQueryAsync(productQuery, ct).ConfigureAwait(false);

        AddCartItemCommand commnad = new(
            Id: id,
            DeliveryType: req.DeliveryType,
            Price: new(price, "BGN"),
            Quantity: req.Quantity,
            ProductId: new(req.ProductId)
        );
        CartItemId itemId = await sender.SendCommandAsync(commnad, ct).ConfigureAwait(false);

        await SendOkAsync(itemId.Value).ConfigureAwait(false);
    }
}
