namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Internal.Purchase.WithDelivery.Data;

public class PurchaseOngoingOrderWithDeliveryInvalidShipmentServiceData : PurchaseOngoingOrderWithDeliveryData
{
    public PurchaseOngoingOrderWithDeliveryInvalidShipmentServiceData()
    {
        Add("payment-method-id-1", 2, string.Empty, "Bulgaria", "Sofia", null, "customcads@gmail.com");
        Add("payment-method-id-2", 5, null!, "Romania", "Bucharest", "+359359359359", null);
    }
}
