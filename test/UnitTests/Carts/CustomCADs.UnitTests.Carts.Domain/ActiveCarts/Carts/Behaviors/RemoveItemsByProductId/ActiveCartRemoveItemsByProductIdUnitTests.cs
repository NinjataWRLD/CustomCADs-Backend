using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Carts.Behaviors.RemoveItemsByProductId;

public class ActiveCartRemoveItemsByProductIdUnitTests : ActiveCartsBaseUnitTests
{
    private readonly ActiveCart cart;

    public ActiveCartRemoveItemsByProductIdUnitTests()
    {
        cart = CreateCartWithItems(true, false, true, true, false);
    }

    [Fact]
    public void RemoveItemsByProductId_ShouldNotThrowException_WhenCartItemFound()
    {
        ProductId[] productIds = [.. cart.Items.Select(i => i.ProductId).Distinct()];
        foreach (ProductId productId in productIds)
        {
            cart.RemoveItemsByProductId(productId);
        }
    }

    [Fact]
    public void RemoveItemsByProductId_ShouldRemoveProperly_WhenCartItemFound()
    {
        ProductId productId = cart.Items.First().ProductId;
        cart.RemoveItemsByProductId(productId);
        Assert.DoesNotContain(cart.Items, i => i.ProductId == productId);
    }
}
