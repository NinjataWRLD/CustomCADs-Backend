using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

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

        foreach (ActiveCartItem item in items.Where(x => !x.ForDelivery))
        {
            cart.AddItem(item.ProductId);
        }
        foreach (ActiveCartItem item in items.Where(x => x.ForDelivery))
        {
            var cartItem = cart.AddItem(item.ProductId, item.CustomizationId!.Value);
            cartItem.IncreaseQuantity(item.Quantity - 1);
        }

        return cart;
    }

    protected static ActiveCartItem CreateItem(
        ActiveCartId? cartId = null,
        ProductId? productId = null
    ) => ActiveCartItem.Create(
            cartId: cartId ?? ValidId1,
            productId: productId ?? CartItemsData.ValidProductId1
        );

    protected static ActiveCartItem CreateItemWithDelivery(
        ActiveCartId? cartId = null,
        ProductId? productId = null,
        CustomizationId? customizationId = null
    ) => ActiveCartItem.Create(
            cartId: cartId ?? ValidId1,
            productId: productId ?? CartItemsData.ValidProductId1,
            customizationId: customizationId ?? CartItemsData.ValidCustomizationId1
        );
}
