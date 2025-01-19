using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Carts.Create.Normal.Data;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Carts.Create.Normal;

public class ActiveCartCreateUnitTests : ActiveCartsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(ActiveCartCreateValidData))]
    public void Create_ShouldNotThrowException(AccountId buyerId)
    {
        CreateCart(buyerId: buyerId);
    }

    [Theory]
    [ClassData(typeof(ActiveCartCreateValidData))]
    public void Create_ShouldPopulatePropertiesProperly(AccountId buyerId)
    {
        var cart = CreateCart(buyerId: buyerId);

        Assert.Multiple(
            () => Assert.Equal(buyerId, cart.BuyerId),
            () => Assert.Empty(cart.Items)
        );
    }
}
