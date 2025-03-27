namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.Normal.Data;

using CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.Normal;
using static ActiveCartsData;

public class PurchaseActiveCartInvalidPaymentMethodIdData : PurchaseActiveCartData
{
    public PurchaseActiveCartInvalidPaymentMethodIdData()
    {
        Add(string.Empty, ValidBuyerId1);
        Add(null!, ValidBuyerId2);
    }
}
