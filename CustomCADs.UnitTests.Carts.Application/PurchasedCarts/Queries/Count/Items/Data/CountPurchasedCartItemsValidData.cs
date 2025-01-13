namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.Count.Items.Data;

using static PurchasedCartsData;

public class CountPurchasedCartItemsValidData : CountPurchasedCartItemsData
{
    public CountPurchasedCartItemsValidData()
    {
        Add(ValidBuyerId1);
        Add(ValidBuyerId2);
    }
}
