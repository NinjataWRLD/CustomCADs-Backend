using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items;

using static ActiveCartsData.CartItemsData;

public class ActiveCartItemsBaseUnitTests
{
    protected static ActiveCartItem CreateItem(
        ActiveCartId? cartId = null,
        ProductId? productId = null
    ) => ActiveCartItem.Create(
            cartId: cartId ?? ActiveCartsData.ValidId1,
            productId: productId ?? ValidProductId1
        );

    protected static ActiveCartItem CreateItemWithDelivery(
        ActiveCartId? cartId = null,
        ProductId? productId = null,
        CustomizationId? customizationId = null
    ) => ActiveCartItem.Create(
            cartId: cartId ?? ActiveCartsData.ValidId1,
            productId: productId ?? ValidProductId1,
            customizationId: customizationId ?? ValidCustomizationId1
        );
}
