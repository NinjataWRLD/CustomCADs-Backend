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
            cart: CreateCartWithItems(1, 1),
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
                cart: CreateCartWithItems(2, 0),
                prices: prices,
                productCads: productCads,
                itemCads: itemCads
            ).SetShipmentId(ValidShipmentId1);
        });
    }

    private static ActiveCart CreateCartWithItems(int noDeliveryCount, int forDeliveryCount)
    {
        ActiveCart cart = ActiveCart.Create(ValidBuyerId1);
        for (int i = 0; i < noDeliveryCount; i++)
        {
            cart.AddItem(CartItemsData.ValidProductId1);
            cart.AddItem(CartItemsData.ValidProductId2);
        }
        for (int i = 0; i < forDeliveryCount; i++)
        {
            cart.AddItem(CartItemsData.ValidProductId1, CartItemsData.ValidCustomizationId1);
            cart.AddItem(CartItemsData.ValidProductId2, CartItemsData.ValidCustomizationId1);
        }

        return cart;
    }
}
