using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.Common.Exceptions.PurchasedCarts.Carts;
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
            (price, itemCadId, CreateActiveCartItem())
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
                (price, itemCadId, CreateActiveCartItem())
            ]);
        }

        Assert.Throws<PurchasedCartValidationException>(() =>
        {
            purchasedCart.AddItems([
                (price, itemCadId, CreateActiveCartItem())
            ]);
        });
    }

    private static ActiveCartItem CreateActiveCartItem()
        => ActiveCartItem.Create(
            cartId: ActiveCartsData.ValidId1,
            productId: ActiveCartsData.CartItemsData.ValidProductId1
        );
}
