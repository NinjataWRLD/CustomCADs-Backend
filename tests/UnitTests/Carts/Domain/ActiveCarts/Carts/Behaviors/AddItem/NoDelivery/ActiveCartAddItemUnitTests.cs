using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.Common.Exceptions.ActiveCarts.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Carts.Behaviors.AddItem.NoDelivery.Data;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Carts.Behaviors.AddItem.NoDelivery;

using static ActiveCartsData;

public class ActiveCartAddItemUnitTests : ActiveCartsBaseUnitTests
{
    public readonly ActiveCart cart = CreateCart();

    [Theory]
    [ClassData(typeof(ActiveCartAddItemValidData))]
    public void AddItem_ShouldNotThrowException_WhenCartItemIsValid(ProductId productId)
    {
        cart.AddItem(productId);
    }

    [Theory]
    [ClassData(typeof(ActiveCartAddItemValidData))]
    public void AddItem_ShouldPopulateItemsProperly(ProductId productId)
    {
        var expected = ActiveCartItem.Create(productId, cart.Id);
        cart.AddItem(productId);

        var actual = cart.Items.Single();
        Assert.Multiple(
            () => Assert.Equal(expected.CartId, actual.CartId),
            () => Assert.Equal(expected.Quantity, actual.Quantity),
            () => Assert.Equal(expected.ProductId, actual.ProductId),
            () => Assert.False(actual.ForDelivery)
        );
    }

    [Theory]
    [ClassData(typeof(ActiveCartAddItemValidCountData))]
    public void AddItem_ShouldAddCorrectCountOfItems(int count)
    {
        for (int i = 0; i < count; i++)
        {
            cart.AddItem(CartItemsData.ValidProductId1);
        }

        Assert.Equal(count, cart.Items.Count);
    }

    [Theory]
    [ClassData(typeof(ActiveCartAddItemInvalidCountData))]
    public void AddItem_ShouldThrowException_WhenCountOfItemsIsInvalid(int count)
    {
        Assert.Throws<ActiveCartValidationException>(() =>
        {
            for (int i = 0; i < count; i++)
            {
                cart.AddItem(CartItemsData.ValidProductId1);
            }
        });
    }
}
