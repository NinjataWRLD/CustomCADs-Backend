using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Carts;

using static ActiveCartsData;

public class ActiveCartsBaseUnitTests
{
    protected static ActiveCart CreateCart(AccountId? buyerId = null)
        => ActiveCart.Create(buyerId ?? ValidBuyerId1);

    protected static ActiveCart CreateCartWithId(ActiveCartId? id = null, AccountId? buyerId = null)
        => ActiveCart.CreateWithId(id ?? ValidId1, buyerId ?? ValidBuyerId1);

    protected static ActiveCart CreateCartWithItems(params bool[] forDeliveries)
    {
        ActiveCart cart = CreateCart();

        foreach (bool forDelivery in forDeliveries)
        {
            cart.AddItemWithId(
                weight: CartItemsData.ValidWeight1,
                productId: CartItemsData.ValidProductId1,
                forDelivery: forDelivery
            );
        }

        return cart;
    }
}
