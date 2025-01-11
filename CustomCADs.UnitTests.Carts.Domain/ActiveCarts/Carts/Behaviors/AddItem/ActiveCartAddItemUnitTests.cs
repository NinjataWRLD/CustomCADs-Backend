using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.Common.Exceptions.ActiveCarts.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Carts.Behaviors.AddItem.Data;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Carts.Behaviors.AddItem;

using static ActiveCartsData;

public class ActiveCartAddItemUnitTests : ActiveCartsBaseUnitTests
{
    public readonly ActiveCart cart = CreateCart();

    [Theory]
    [ClassData(typeof(ActiveCartAddItemValidData))]
    public void AddItem_ShouldNotThrowException_WhenCartItemIsValid(double weight, ProductId productId, bool delivery)
    {
        cart.AddItem(weight, productId, delivery);
    }

    [Theory]
    [ClassData(typeof(ActiveCartAddItemValidData))]
    public void AddItem_ShouldPopulateItemsProperly(double weight, ProductId productId, bool forDelivery)
    {
        var expected = ActiveCartItem.Create(
            weight: weight,
            productId: productId,
            cartId: cart.Id,
            forDelivery: forDelivery
        );
        cart.AddItem(weight, productId, forDelivery);

        var actual = cart.Items.Single();
        Assert.Multiple(
            () => Assert.Equal(expected.CartId, actual.CartId),
            () => Assert.Equal(expected.Quantity, actual.Quantity),
            () => Assert.Equal(expected.Weight, actual.Weight),
            () => Assert.Equal(expected.ProductId, actual.ProductId),
            () => Assert.Equal(expected.ForDelivery, actual.ForDelivery)
        );
    }

    [Theory]
    [ClassData(typeof(ActiveCartAddItemValidCountData))]
    public void AddItem_ShouldAddCorrectCountOfItems(int count)
    {
        for (int i = 0; i < count; i++)
        {
            cart.AddItem(
                weight: CartItemsData.ValidWeight1,
                productId: CartItemsData.ValidProductId1,
                forDelivery: true
            );
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
                cart.AddItem(
                    weight: CartItemsData.ValidWeight1,
                    productId: CartItemsData.ValidProductId1,
                    forDelivery: true
                );
            }
        });
    }
}
