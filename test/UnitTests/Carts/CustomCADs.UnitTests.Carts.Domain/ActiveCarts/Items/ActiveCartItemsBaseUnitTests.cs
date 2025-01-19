using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items;

using static ActiveCartsData.CartItemsData;

public class ActiveCartItemsBaseUnitTests
{
    protected static ActiveCartItem CreateItem(
        ActiveCartId? cartId = null,
        ProductId? productId = null,
        double? weight = null,
        bool? forDelivery = null
    ) => ActiveCartItem.Create(
            cartId: cartId ?? ActiveCartsData.ValidId1,
            productId: productId ?? ValidProductId1,
            weight: weight ?? ValidWeight1,
            forDelivery: forDelivery ?? false
        );

    protected static ActiveCartItem CreateItemWithId(
        ActiveCartItemId? id = null,
        ActiveCartId? cartId = null,
        ProductId? productId = null,
        double? weight = null,
        bool? forDelivery = null
    ) => ActiveCartItem.CreateWithId(
            id: id ?? ValidId1,
            cartId: cartId ?? ActiveCartsData.ValidId1,
            productId: productId ?? ValidProductId1,
            weight: weight ?? ValidWeight1,
            forDelivery: forDelivery ?? false
        );
}
