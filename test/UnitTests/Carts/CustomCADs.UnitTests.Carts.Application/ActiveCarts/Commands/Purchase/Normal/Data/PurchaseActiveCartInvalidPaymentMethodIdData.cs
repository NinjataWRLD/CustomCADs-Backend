namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Purchase.Normal.Data;

using static ActiveCartsData;

public class PurchaseActiveCartInvalidPaymentMethodIdData : PurchaseActiveCartData
{
    public PurchaseActiveCartInvalidPaymentMethodIdData()
    {
        Add(string.Empty, ValidBuyerId1);
        Add(null!, ValidBuyerId2);
    }
}
