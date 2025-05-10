using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Carts.Behaviors.AddItems;

using Data;
using static PurchasedCartsData.CartItemsData;
using static PurchasedCartConstants;

public class PurchasedCartAddItemsUnitTests : PurchasedCartsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(PurchasedCartAddItemsValidData))]
    public void AddItems_ShouldNotThrow_WhenItemsCountIsValid(decimal price)
    {
        CreateCartWithId().AddItems([
            (price, ValidCadId, ValidProductId, false, null, 1, DateTimeOffset.UtcNow)
        ]);
    }

    [Theory]
    [ClassData(typeof(PurchasedCartAddItemsValidData))]
    public void AddItems_ShouldThrow_WhenItemsCountIsNotValid(decimal price)
    {
        var purchasedCart = CreateCartWithId();
        for (int i = 0; i < ItemsCountMax; i++)
        {
            purchasedCart.AddItems([
                (price, ValidCadId, ProductId.New(), false, null, 1, DateTimeOffset.UtcNow)
            ]);
        }

        Assert.Throws<CustomValidationException<PurchasedCart>>(() =>
        {
            purchasedCart.AddItems([
                (price, ValidCadId, ProductId.New(), false, null, 1, DateTimeOffset.UtcNow)
            ]);
        });
    }
}
