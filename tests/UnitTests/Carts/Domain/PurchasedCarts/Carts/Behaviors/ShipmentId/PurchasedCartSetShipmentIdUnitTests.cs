using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Carts.Behaviors.ShipmentId;

using static PurchasedCartsData;

public class PurchasedCartSetShipmentIdUnitTests : PurchasedCartsBaseUnitTests
{
    private static readonly ProductId productId1 = ProductId.New();
    private static readonly ProductId productId2 = ProductId.New();
    private static readonly CadId cadId1 = CadId.New();
    private static readonly CadId cadId2 = CadId.New();


    private static readonly Dictionary<ProductId, decimal> prices = new()
    {
        [productId1] = CartItemsData.ValidPrice1,
        [productId2] = CartItemsData.ValidPrice2
    };
    private static readonly Dictionary<ProductId, CadId> productCads = new()
    {
        [productId1] = cadId1,
        [productId2] = cadId2
    };
    private static readonly Dictionary<CadId, CadId> itemCads = new()
    {
        [cadId1] = cadId2,
        [cadId2] = cadId1
    };


    [Fact]
    public void SetShipmentId_ShouldNotThrowException_WhenIsDelivery()
    {
        CreateCartWithItems(
            buyerId: ValidBuyerId,
            items: CreateItems(1, 1),
            prices: prices,
            productCads: productCads,
            itemCads: itemCads
        ).SetShipmentId(ValidShipmentId);
    }

    [Fact]
    public void SetShipmentId_ShouldThrowException_WhenNotDelivery()
    {
        Assert.Throws<CustomValidationException<PurchasedCart>>(() =>
        {
            CreateCartWithItems(
                buyerId: ValidBuyerId,
                items: CreateItems(2, 0),
                prices: prices,
                productCads: productCads,
                itemCads: itemCads
            ).SetShipmentId(ValidShipmentId);
        });
    }

    private static ActiveCartItem[] CreateItems(int noDeliveryCount, int forDeliveryCount)
    {
        List<ActiveCartItem> items = [];

        for (int i = 0; i < noDeliveryCount; i++)
        {
            items.Add(ActiveCartItem.Create(productId1, ValidBuyerId));
            items.Add(ActiveCartItem.Create(productId2, ValidBuyerId));
        }
        for (int i = 0; i < forDeliveryCount; i++)
        {
            items.Add(ActiveCartItem.Create(productId1, ValidBuyerId, CartItemsData.ValidCustomizationId));
            items.Add(ActiveCartItem.Create(productId2, ValidBuyerId, CartItemsData.ValidCustomizationId));
        }

        return [.. items];
    }
}
