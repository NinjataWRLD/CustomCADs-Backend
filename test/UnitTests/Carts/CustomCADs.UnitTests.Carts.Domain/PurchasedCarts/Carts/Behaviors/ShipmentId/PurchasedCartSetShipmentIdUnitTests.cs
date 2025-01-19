using CustomCADs.Carts.Domain.Common.Exceptions.PurchasedCarts.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Carts.Behaviors.ShipmentId.Data;

namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Carts.Behaviors.ShipmentId;

using static PurchasedCartsData;

public class PurchasedCartSetShipmentIdUnitTests : PurchasedCartsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(PurchasedCartSetShipmentIdValidData))]
    public void SetShipmentId_ShouldNotThrowException_WhenIsDelivery(Dictionary<ProductId, decimal> prices, Dictionary<ProductId, CadId> productCads, Dictionary<CadId, CadId> itemCads)
    {
        CreateCartWithItems(
            cart: SeedActiveCart(false, true),
            prices: prices,
            productCads: productCads,
            itemCads: itemCads
        ).SetShipmentId(ValidShipmentId1);
    }

    [Theory]
    [ClassData(typeof(PurchasedCartSetShipmentIdValidData))]
    public void SetShipmentId_ShouldThrowException_WhenNotDelivery(Dictionary<ProductId, decimal> prices, Dictionary<ProductId, CadId> productCads, Dictionary<CadId, CadId> itemCads)
    {
        Assert.Throws<PurchasedCartValidationException>(() =>
        {
            CreateCartWithItems(
                cart: SeedActiveCart(false, false),
                prices: prices,
                productCads: productCads,
                itemCads: itemCads
            ).SetShipmentId(ValidShipmentId1);
        });
    }

    private ActiveCart SeedActiveCart(params bool[] forDeliveries)
    {
        ActiveCart cart = ActiveCart.Create(ValidBuyerId1);
        foreach (bool forDelivery in forDeliveries)
        {
            cart.AddItem(
                weight: ActiveCartsData.CartItemsData.ValidWeight1,
                productId: CartItemsData.ValidProductId1,
                forDelivery: forDelivery
            );
            cart.AddItem(
                weight: ActiveCartsData.CartItemsData.ValidWeight2,
                productId: CartItemsData.ValidProductId2,
                forDelivery: forDelivery
            );
        }

        return cart;
    }
}
