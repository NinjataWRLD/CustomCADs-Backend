using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.Common.Exceptions.ActiveCarts.CartItems;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Carts.Behaviors.RemoveItem;

public class ActiveCartRemoveItemUnitTests : ActiveCartsBaseUnitTests
{
    private readonly ActiveCart cart = CreateCartWithItems(true, false);

    [Fact]
    public void RemoveItem_ShouldNotThrowException_WhenCartItemFound()
    {
        ActiveCartItemId[] itemsIds = [.. cart.Items.Select(x => x.Id)];
        foreach (var itemId in itemsIds)
        {
            cart.RemoveItem(itemId);
        }
    }

    [Fact]
    public void RemoveItem_ShouldRemoveProperly_WhenCartItemFound()
    {
        ActiveCartItem item = cart.Items.First();
        cart.RemoveItem(item.Id);
        Assert.DoesNotContain(cart.Items, i => i.Id == item.Id);
    }

    [Fact]
    public void RemoveItem_ShouldThrowException_WhenCartItemNotFound()
    {
        Assert.Throws<ActiveCartItemNotFoundException>(() =>
        {
            CreateCart().RemoveItem(ActiveCartItemId.New());
        });
    }
}
