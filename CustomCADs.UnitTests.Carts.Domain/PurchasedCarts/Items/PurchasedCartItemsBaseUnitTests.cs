using CustomCADs.Carts.Domain.PurchasedCarts.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items;

using static PurchasedCartsData.CartItemsData;

public class PurchasedCartItemsBaseUnitTests
{
    protected static PurchasedCartItem CreateItem(
        PurchasedCartId? cartId = null,
        ProductId? productId = null,
        CadId? cadId = null,
        decimal? price = null,
        int? quantity = null,
        bool? forDelivery = null
    ) => PurchasedCartItem.Create(
            cartId: cartId ?? PurchasedCartsData.ValidId1,
            productId: productId ?? ValidProductId1,
            cadId: cadId ?? ValidCadId1,
            price: price ?? ValidPrice1,
            quantity: quantity ?? ValidQuantity1,
            forDelivery: forDelivery ?? false
        );

    protected static PurchasedCartItem CreateItemWithId(
        PurchasedCartItemId? id = null,
        PurchasedCartId? cartId = null,
        ProductId? productId = null,
        CadId? cadId = null,
        decimal? price = null,
        int? quantity = null,
        bool? forDelivery = null
    ) => PurchasedCartItem.CreateWithId(
            id: id ?? PurchasedCartItemId.New(),
            cartId: cartId ?? PurchasedCartsData.ValidId1,
            productId: productId ?? ValidProductId1,
            cadId: cadId ?? ValidCadId1,
            price: price ?? ValidPrice1,
            quantity: quantity ?? ValidQuantity1,
            forDelivery: forDelivery ?? false
        );
}
