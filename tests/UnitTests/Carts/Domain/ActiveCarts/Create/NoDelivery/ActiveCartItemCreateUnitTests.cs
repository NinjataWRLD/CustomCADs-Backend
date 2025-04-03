using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Create.NoDelivery.Data;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Create.NoDelivery;

public class ActiveCartItemCreateUnitTests : ActiveCartItemsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(ActiveCartItemCreateValidData))]
    public void Create_ShouldNotThrow_WhenCartIsValid(AccountId buyerId, ProductId productId)
    {
        CreateItem(
            buyerId: buyerId,
            productId: productId
        );
    }

    [Theory]
    [ClassData(typeof(ActiveCartItemCreateValidData))]
    public void Create_ShouldPopulateProperties(AccountId buyerId, ProductId productId)
    {
        var item = CreateItem(
            buyerId: buyerId,
            productId: productId
        );

        Assert.Multiple(
            () => Assert.Equal(buyerId, item.BuyerId),
            () => Assert.Equal(productId, item.ProductId),
            () => Assert.False(item.ForDelivery)
        );
    }
}
