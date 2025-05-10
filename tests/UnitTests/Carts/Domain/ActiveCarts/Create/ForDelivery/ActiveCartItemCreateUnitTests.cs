namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Create.ForDelivery;

using static ActiveCartsData;

public class ActiveCartItemCreateUnitTests : ActiveCartItemsBaseUnitTests
{
    [Fact]
    public void Create_ShouldNotThrow_WhenCartIsValid()
    {
        CreateItemWithDelivery(
            buyerId: ValidBuyerId,
            productId: ValidProductId,
            customizationId: ValidCustomizationId
        );
    }

    [Fact]
    public void Create_ShouldPopulateProperties()
    {
        var item = CreateItemWithDelivery(
            buyerId: ValidBuyerId,
            productId: ValidProductId,
            customizationId: ValidCustomizationId
        );

        Assert.Multiple(
            () => Assert.Equal(ValidBuyerId, item.BuyerId),
            () => Assert.Equal(ValidProductId, item.ProductId),
            () => Assert.Equal(ValidCustomizationId, item.CustomizationId),
            () => Assert.True(item.ForDelivery)
        );
    }
}
