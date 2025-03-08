using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Carts;

using static ActiveCartsData;

public class ActiveCartsBaseUnitTests
{
    protected static ActiveCart CreateCart(AccountId? buyerId = null)
        => ActiveCart.Create(buyerId ?? ValidBuyerId1);

    protected static ActiveCart CreateCartWithId(ActiveCartId? id = null, AccountId? buyerId = null)
        => ActiveCart.CreateWithId(id ?? ValidId1, buyerId ?? ValidBuyerId1);

    protected static ActiveCart CreateCartWithItems(int noDeliveryCount, int forDeliveryCount)
    {
        ActiveCart cart = CreateCart();

        for (int i = 0; i < noDeliveryCount; ++i)
        {
            cart.AddItem(ProductId.New());
        }
        for (int i = 0; i < forDeliveryCount; ++i)
        {
            cart.AddItem(ProductId.New(), CustomizationId.New());
        }
        return cart;
    }
}
