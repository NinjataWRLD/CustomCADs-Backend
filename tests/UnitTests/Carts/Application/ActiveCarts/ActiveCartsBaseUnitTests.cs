using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts;

using static ActiveCartsData;

public class ActiveCartsBaseUnitTests
{
    protected static readonly CancellationToken ct = CancellationToken.None;

    protected static ActiveCart CreateCart(AccountId? buyerId = null)
        => ActiveCart.Create(buyerId ?? ValidBuyerId1);

    protected static ActiveCart CreateCartWithId(ActiveCartId? id = null, AccountId? buyerId = null)
        => ActiveCart.CreateWithId(
            id: id ?? ValidId1,
            buyerId: buyerId ?? ValidBuyerId1
        );

    protected static ActiveCart CreateCartWithItems(ActiveCartId? id = null, AccountId? buyerId = null, params ActiveCartItem[] items)
    {
        var cart = CreateCartWithId(id, buyerId);
        for (int i = 0; i < items.Length; i++)
        {
            ActiveCartItem item = items[i];
            cart.AddItem(item.Weight, item.ProductId, item.ForDelivery);

            var cartItem = cart.Items.ElementAt(i);
            if (cartItem.ForDelivery)
            {
                cartItem.IncreaseQuantity(item.Quantity - 1);
            }
        }

        return cart;
    }

    protected static ActiveCartItem CreateItem(
        ActiveCartId? cartId = null,
        ProductId? productId = null,
        double? weight = null,
        bool? forDelivery = null
    ) => ActiveCartItem.Create(
            cartId: cartId ?? ValidId1,
            productId: productId ?? CartItemsData.ValidProductId1,
            weight: weight ?? CartItemsData.ValidWeight1,
            forDelivery: forDelivery ?? false
        );
}
