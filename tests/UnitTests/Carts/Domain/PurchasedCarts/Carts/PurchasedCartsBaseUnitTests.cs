using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Carts;

using static PurchasedCartsData;

public class PurchasedCartsBaseUnitTests
{
    protected static PurchasedCart CreateCart(AccountId? buyerId = null)
        => PurchasedCart.Create(buyerId ?? ValidBuyerId1);

    protected static PurchasedCart CreateCartWithId(PurchasedCartId? id = null, AccountId? buyerId = null)
        => PurchasedCart.CreateWithId(id ?? ValidId1, buyerId ?? ValidBuyerId1);

    protected static PurchasedCart CreateCartWithItems(
        ActiveCart cart,
        Dictionary<ProductId, decimal> prices,
        Dictionary<ProductId, CadId> productCads,
        Dictionary<CadId, CadId> itemCads
    )
    {
        var purchasedCart = CreateCart(cart.BuyerId);

        purchasedCart.AddItems([.. cart.Items.Select(item => {
            decimal price = prices[item.ProductId];
            CadId productCadId = productCads[item.ProductId];
            CadId itemCadId = itemCads[productCadId];

            return (price, itemCadId, item);
        })]);

        return purchasedCart;
    }
}
