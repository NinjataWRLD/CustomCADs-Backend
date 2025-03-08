using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Create.NoDelivery.Data;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Create.NoDelivery;

public class ActiveCartItemCreateUnitTests : ActiveCartItemsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(ActiveCartItemCreateValidData))]
    public void Create_ShouldNotThrow_WhenCartIsValid(ActiveCartId cartId, ProductId productId)
    {
        CreateItem(
            cartId: cartId,
            productId: productId
        );
    }

    [Theory]
    [ClassData(typeof(ActiveCartItemCreateValidData))]
    public void Create_ShouldPopulateProperties(ActiveCartId cartId, ProductId productId)
    {
        var item = CreateItem(
            cartId: cartId,
            productId: productId
        );

        Assert.Multiple(
            () => Assert.Equal(cartId, item.CartId),
            () => Assert.Equal(productId, item.ProductId),
            () => Assert.False(item.ForDelivery)
        );
    }
}
