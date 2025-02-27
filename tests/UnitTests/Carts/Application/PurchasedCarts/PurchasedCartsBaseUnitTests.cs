using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.PurchasedCarts.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts;

using static PurchasedCartsData;
using static PurchasedCartsData.CartItemsData;

public class PurchasedCartsBaseUnitTests
{
    protected static readonly CancellationToken ct = CancellationToken.None;

    protected static PurchasedCart CreateCart(AccountId? buyerId = null)
        => PurchasedCart.Create(buyerId ?? ValidBuyerId1);

    protected static PurchasedCart CreateCartWithId(PurchasedCartId? id = null, AccountId? buyerId = null)
        => PurchasedCart.CreateWithId(
            id: id ?? PurchasedCartsData.ValidId1,
            buyerId: buyerId ?? ValidBuyerId1
        );

    protected static PurchasedCart CreateCartWithItems(PurchasedCartId? id = null, AccountId? buyerId = null, params PurchasedCartItem[] items)
    {
        var purchasedCart = CreateCartWithId(id, buyerId);
        purchasedCart.AddItems([.. items.Select(i =>
            (Price: i.Price,
            CadId: i.CadId,
            Item: ActiveCartItem.Create(
                cartId: ActiveCartsData.ValidId1,
                productId: i.ProductId,
                forDelivery: i.ForDelivery,
                weight: ActiveCartsData.CartItemsData.ValidWeight1
            ))
        )]);

        return purchasedCart;
    }

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
