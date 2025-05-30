namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.Normal.Data;

public class PurchaseActiveCartInvalidPaymentMethodIdData : PurchaseActiveCartData
{
    public PurchaseActiveCartInvalidPaymentMethodIdData()
    {
        Add(string.Empty);
        Add(null!);
    }
}
