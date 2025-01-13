namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Purchase.Normal.Data;

public class PurchaseOngoingOrderInvalidPaymentMethodIdData : PurchaseOngoingOrderData
{
    public PurchaseOngoingOrderInvalidPaymentMethodIdData()
    {
        Add(string.Empty);
        Add(null!);
    }
}
