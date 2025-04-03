using CustomCADs.Shared.Core.Common.Exceptions.Domain;
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
            buyerId: ValidBuyerId1,
            items: CreateItems(1, 1),
            prices: prices,
            productCads: productCads,
            itemCads: itemCads
        ).SetShipmentId(ValidShipmentId1);
    }

    [Theory]
    [ClassData(typeof(PurchasedCartSetShipmentIdValidData))]
    public void SetShipmentId_ShouldThrowException_WhenNotDelivery(Dictionary<ProductId, decimal> prices, Dictionary<ProductId, CadId> productCads, Dictionary<CadId, CadId> itemCads)
    {
        Assert.Throws<CustomValidationException<PurchasedCart>>(() =>
        {
            CreateCartWithItems(
                buyerId: ValidBuyerId1,
                items: CreateItems(2, 0),
                prices: prices,
                productCads: productCads,
                itemCads: itemCads
            ).SetShipmentId(ValidShipmentId1);
        });
    }

    private static ActiveCartItem[] CreateItems(int noDeliveryCount, int forDeliveryCount)
    {
        List<ActiveCartItem> items = [];

        for (int i = 0; i < noDeliveryCount; i++)
        {
            items.Add(ActiveCartItem.Create(CartItemsData.ValidProductId1, ValidBuyerId1));
            items.Add(ActiveCartItem.Create(CartItemsData.ValidProductId2, ValidBuyerId1));
        }
        for (int i = 0; i < forDeliveryCount; i++)
        {
            items.Add(ActiveCartItem.Create(CartItemsData.ValidProductId1, ValidBuyerId1, CartItemsData.ValidCustomizationId1));
            items.Add(ActiveCartItem.Create(CartItemsData.ValidProductId2, ValidBuyerId1, CartItemsData.ValidCustomizationId1));
        }

        return [.. items];
    }
}
