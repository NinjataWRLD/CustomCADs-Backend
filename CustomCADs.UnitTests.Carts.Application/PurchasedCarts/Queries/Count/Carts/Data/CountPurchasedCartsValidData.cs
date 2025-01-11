namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.Count.Carts.Data;

using static PurchasedCartsData;

public class CountPurchasedCartsValidData : CountPurchasedCartsData
{
    public CountPurchasedCartsValidData()
    {
        Add(ValidBuyerId1);
        Add(ValidBuyerId2);
    }
}
