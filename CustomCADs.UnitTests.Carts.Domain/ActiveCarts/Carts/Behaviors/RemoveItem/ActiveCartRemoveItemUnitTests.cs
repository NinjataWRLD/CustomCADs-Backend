using CustomCADs.Carts.Domain.ActiveCarts.Entities;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Carts.Behaviors.RemoveItem;

public class ActiveCartRemoveItemUnitTests : ActiveCartsBaseUnitTests
{
    private readonly ActiveCart cart = CreateCartWithItems(true, false);

    [Fact]
    public void RemoveItem_ShouldNotThrowException_WhenCartItemFound()
    {
        foreach (ActiveCartItem item in cart.Items)
        {
            cart.RemoveItem(item);
        }
    }

    [Fact]
    public void RemoveItem_ShouldRemoveProperly_WhenCartItemFound()
    {
        ActiveCartItem item = cart.Items.First();
        cart.RemoveItem(item);
        Assert.DoesNotContain(cart.Items, i => i.Id == item.Id);
    }
}
