namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Carts.Create.WithId.Data;

using static PurchasedCartsData;

public class PurchasedCartCreateWithIdValidData : PurchasedCartCreateWithIdData
{
    public PurchasedCartCreateWithIdValidData()
    {
        Add(ValidId1, ValidBuyerId1);
        Add(ValidId2, ValidBuyerId2);
    }
}