using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Carts.Create.WithId.Data;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Carts.Create.WithId;

public class ActiveCartCreateWithIdHandlerUnitTests : ActiveCartsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(ActiveCartCreateWithIdValidData))]
    public void CreateWithId_ShouldNotThrowException(ActiveCartId id, AccountId buyerId)
    {
        CreateCartWithId(id, buyerId);
    }

    [Theory]
    [ClassData(typeof(ActiveCartCreateWithIdValidData))]
    public void CreateWithId_ShouldPopulatePropertiesProperly(ActiveCartId id, AccountId buyerId)
    {
        var cart = CreateCartWithId(id, buyerId);

        Assert.Multiple(
            () => Assert.Equal(id, cart.Id),
            () => Assert.Equal(buyerId, cart.BuyerId),
            () => Assert.Empty(cart.Items)
        );
    }
}
