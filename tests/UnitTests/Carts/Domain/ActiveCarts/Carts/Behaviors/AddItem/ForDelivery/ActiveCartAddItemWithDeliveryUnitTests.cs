using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Carts.Behaviors.AddItem.ForDelivery.Data;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Carts.Behaviors.AddItem.ForDelivery;

using static ActiveCartsData;

public class ActiveCartAddItemWithDeliveryUnitTests : ActiveCartsBaseUnitTests
{
    public readonly ActiveCart cart = CreateCart();

    [Theory]
    [ClassData(typeof(ActiveCartAddItemWithDeliveryValidData))]
    public void AddItem_ShouldNotThrowException_WhenCartItemIsValid(ProductId productId, CustomizationId customizationId)
    {
        cart.AddItem(productId, customizationId);
    }

    [Theory]
    [ClassData(typeof(ActiveCartAddItemWithDeliveryValidData))]
    public void AddItem_ShouldPopulateItemsProperly(ProductId productId, CustomizationId customizationId)
    {
        var expected = ActiveCartItem.Create(productId, cart.Id, customizationId);
        cart.AddItem(productId, customizationId);

        var actual = cart.Items.Single();
        Assert.Multiple(
            () => Assert.Equal(expected.CartId, actual.CartId),
            () => Assert.Equal(expected.Quantity, actual.Quantity),
            () => Assert.Equal(expected.ProductId, actual.ProductId),
            () => Assert.Equal(expected.CustomizationId, actual.CustomizationId),
            () => Assert.True(actual.ForDelivery)
        );
    }

    [Theory]
    [ClassData(typeof(ActiveCartAddItemWithDeliveryValidCountData))]
    public void AddItem_ShouldAddCorrectCountOfItems(int count)
    {
        for (int i = 0; i < count; i++)
        {
            cart.AddItem(CartItemsData.ValidProductId1, CartItemsData.ValidCustomizationId1);
        }

        Assert.Equal(count, cart.Items.Count);
    }

    [Theory]
    [ClassData(typeof(ActiveCartAddItemWithDeliveryInvalidCountData))]
    public void AddItem_ShouldThrowException_WhenCountOfItemsIsInvalid(int count)
    {
        Assert.Throws<CustomValidationException<ActiveCart>>(() =>
        {
            for (int i = 0; i < count; i++)
            {
                cart.AddItem(CartItemsData.ValidProductId1, CartItemsData.ValidCustomizationId1);
            }
        });
    }
}
