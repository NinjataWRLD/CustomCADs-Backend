using CustomCADs.Carts.Domain.Common.Exceptions.ActiveCarts.CartItems;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Create.Normal.Data;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Create.Normal;

public class ActiveCartItemCreateUnitTests : ActiveCartItemsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(ActiveCartItemCreateValidData))]
    public void Create_ShouldNotThrow_WhenCartIsValid(ActiveCartId cartId, ProductId productId, double weight, bool forDelivery)
    {
        CreateItem(
            cartId: cartId,
            productId: productId,
            weight: weight,
            forDelivery: forDelivery
        );
    }

    [Theory]
    [ClassData(typeof(ActiveCartItemCreateValidData))]
    public void Create_ShouldPopulateProperties(ActiveCartId cartId, ProductId productId, double weight, bool forDelivery)
    {
        var item = CreateItem(
            cartId: cartId,
            productId: productId,
            weight: weight,
            forDelivery: forDelivery
        );

        Assert.Multiple(
            () => Assert.Equal(cartId, item.CartId),
            () => Assert.Equal(productId, item.ProductId),
            () => Assert.Equal(weight, item.Weight),
            () => Assert.Equal(forDelivery, item.ForDelivery)
        );
    }

    [Theory]
    [ClassData(typeof(ActiveCartItemCreateInvalidWeightData))]
    public void Create_ShouldThrow_WhenCartIsNotValid(ActiveCartId cartId, ProductId productId, double weight, bool forDelivery)
    {
        Assert.Throws<ActiveCartItemValidationException>(() =>
        {
            CreateItem(
                cartId: cartId,
                productId: productId,
                weight: weight,
                forDelivery: forDelivery
            );
        });
    }
}
