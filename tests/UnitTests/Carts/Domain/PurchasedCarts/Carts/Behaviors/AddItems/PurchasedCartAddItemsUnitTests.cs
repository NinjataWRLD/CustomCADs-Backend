using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Carts.Behaviors.AddItems.Data;

namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Carts.Behaviors.AddItems;

using static PurchasedCartConstants;

public class PurchasedCartAddItemsUnitTests : PurchasedCartsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(PurchasedCartAddItemsValidData))]
    public void AddItems_ShouldNotThrow_WhenItemsCountIsValid(decimal price, CadId itemCadId)
    {
        CreateCartWithId().AddItems([
            (price, itemCadId, ProductId.New(), false, null, 1, DateTimeOffset.UtcNow)
        ]);
    }

    [Theory]
    [ClassData(typeof(PurchasedCartAddItemsValidData))]
    public void AddItems_ShouldThrow_WhenItemsCountIsNotValid(decimal price, CadId itemCadId)
    {
        var purchasedCart = CreateCartWithId();
        for (int i = 0; i < ItemsCountMax; i++)
        {
            purchasedCart.AddItems([
                (price, itemCadId, ProductId.New(), false, null, 1, DateTimeOffset.UtcNow)
            ]);
        }

        Assert.Throws<CustomValidationException<PurchasedCart>>(() =>
        {
            purchasedCart.AddItems([
                (price, itemCadId, ProductId.New(), false, null, 1, DateTimeOffset.UtcNow)
            ]);
        });
    }
}
