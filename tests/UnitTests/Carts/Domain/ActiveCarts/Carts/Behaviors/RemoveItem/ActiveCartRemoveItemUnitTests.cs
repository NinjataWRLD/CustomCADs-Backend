using CustomCADs.Carts.Domain.ActiveCarts.Entities;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Carts.Behaviors.RemoveItem;

public class ActiveCartRemoveItemUnitTests : ActiveCartsBaseUnitTests
{
    private readonly ActiveCart cart = CreateCartWithItems(1, 1);

    [Fact]
    public void RemoveItem_ShouldNotThrowException_WhenCartItemFound()
    {
        ActiveCartItem[] items = [.. cart.Items];
        foreach (ActiveCartItem item in items)
        {
            cart.RemoveItem(item);
        }
    }

    [Fact]
    public void RemoveItem_ShouldRemoveProperly_WhenCartItemFound()
    {
        ActiveCartItem item = cart.Items.First();
        cart.RemoveItem(item);
        Assert.DoesNotContain(cart.Items, i => i.ProductId == item.ProductId);
    }
}
