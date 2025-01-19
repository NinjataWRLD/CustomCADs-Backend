using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Carts.Create.WithId.Data;

namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Carts.Create.WithId;

public class PurchasedCartCreateWithIdUnitTests : PurchasedCartsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(PurchasedCartCreateWithIdValidData))]
    public void CreateWithId_ShouldNotThrowException(PurchasedCartId id, AccountId buyerId)
    {
        CreateCartWithId(id, buyerId);
    }

    [Theory]
    [ClassData(typeof(PurchasedCartCreateWithIdValidData))]
    public void CreateWithId_ShouldPopulatePropertiesProperly(PurchasedCartId id, AccountId buyerId)
    {
        var cart = CreateCartWithId(id, buyerId);

        Assert.Multiple(
            () => Assert.Equal(id, cart.Id),
            () => Assert.Equal(buyerId, cart.BuyerId),
            () => Assert.Empty(cart.Items),
            () => Assert.True(DateTime.UtcNow - cart.PurchaseDate < TimeSpan.FromSeconds(1))
        );
    }
}
