using CustomCADs.Carts.Domain.Common.Exceptions.ActiveCarts.CartItems;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Create.WithId.Data;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Create.WithId;

public class ActiveCartItemCreateWithIdUnitTests : ActiveCartItemsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(ActiveCartItemCreateWithIdValidData))]
    public void Create_ShouldNotThrow_WhenCartIsValid(ActiveCartItemId id, ActiveCartId cartId, ProductId productId, double weight, bool forDelivery)
    {
        CreateItemWithId(
            id: id,
            cartId: cartId,
            productId: productId,
            weight: weight,
            forDelivery: forDelivery
        );
    }

    [Theory]
    [ClassData(typeof(ActiveCartItemCreateWithIdValidData))]
    public void Create_ShouldPopulateProperties(ActiveCartItemId id, ActiveCartId cartId, ProductId productId, double weight, bool forDelivery)
    {
        var item = CreateItemWithId(
            id: id,
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
    [ClassData(typeof(ActiveCartItemCreateWithIdInvalidWeightData))]
    public void Create_ShouldThrow_WhenCartIsNotValid(ActiveCartItemId id, ActiveCartId cartId, ProductId productId, double weight, bool forDelivery)
    {
        Assert.Throws<ActiveCartItemValidationException>(() =>
        {
            CreateItemWithId(
                id: id,
                cartId: cartId,
                productId: productId,
                weight: weight,
                forDelivery: forDelivery
            );
        });
    }
}
