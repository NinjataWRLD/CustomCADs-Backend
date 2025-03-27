using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Carts.Create.Normal.Data;

namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Carts.Create.Normal;

public class PurchasedCartCreateUnitTests : PurchasedCartsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(PurchasedCartCreateValidData))]
    public void Create_ShouldNotThrowException(AccountId buyerId)
    {
        CreateCart(buyerId: buyerId);
    }

    [Theory]
    [ClassData(typeof(PurchasedCartCreateValidData))]
    public void Create_ShouldPopulatePropertiesProperly(AccountId buyerId)
    {
        var cart = CreateCart(buyerId: buyerId);

        Assert.Multiple(
            () => Assert.Equal(buyerId, cart.BuyerId),
            () => Assert.Empty(cart.Items),
            () => Assert.True(DateTimeOffset.UtcNow - cart.PurchasedAt < TimeSpan.FromSeconds(1))
        );
    }
}
