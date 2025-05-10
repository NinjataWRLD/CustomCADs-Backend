namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Create.NoDelivery;

using static ActiveCartsData;

public class ActiveCartItemCreateUnitTests : ActiveCartItemsBaseUnitTests
{
    [Fact]
    public void Create_ShouldNotThrow_WhenCartIsValid()
    {
        CreateItem(
            buyerId: ValidBuyerId,
            productId: ValidProductId
        );
    }

    [Fact]
    public void Create_ShouldPopulateProperties()
    {
        var item = CreateItem(
            buyerId: ValidBuyerId,
            productId: ValidProductId
        );

        Assert.Multiple(
            () => Assert.Equal(ValidBuyerId, item.BuyerId),
            () => Assert.Equal(ValidProductId, item.ProductId),
            () => Assert.False(item.ForDelivery)
        );
    }
}
