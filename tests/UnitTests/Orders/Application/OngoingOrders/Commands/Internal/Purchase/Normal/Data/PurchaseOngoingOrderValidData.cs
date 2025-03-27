namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Internal.Purchase.Normal.Data;

public class PurchaseOngoingOrderValidData : PurchaseOngoingOrderData
{
    public PurchaseOngoingOrderValidData()
    {
        Add("payment-method-id-1");
        Add("payment-method-id-2");
    }
}
