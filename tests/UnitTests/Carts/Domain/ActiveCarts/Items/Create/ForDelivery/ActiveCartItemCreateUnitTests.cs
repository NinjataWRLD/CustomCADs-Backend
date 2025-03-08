using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Create.ForDelivery.Data;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Create.ForDelivery;

public class ActiveCartItemCreateUnitTests : ActiveCartItemsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(ActiveCartItemCreateValidData))]
    public void Create_ShouldNotThrow_WhenCartIsValid(ActiveCartId cartId, ProductId productId, CustomizationId customizationId)
    {
        CreateItemWithDelivery(
            cartId: cartId,
            productId: productId,
            customizationId: customizationId
        );
    }

    [Theory]
    [ClassData(typeof(ActiveCartItemCreateValidData))]
    public void Create_ShouldPopulateProperties(ActiveCartId cartId, ProductId productId, CustomizationId customizationId)
    {
        var item = CreateItemWithDelivery(
            cartId: cartId,
            productId: productId,
            customizationId: customizationId
        );

        Assert.Multiple(
            () => Assert.Equal(cartId, item.CartId),
            () => Assert.Equal(productId, item.ProductId),
            () => Assert.Equal(customizationId, item.CustomizationId),
            () => Assert.True(item.ForDelivery)
        );
    }
}
