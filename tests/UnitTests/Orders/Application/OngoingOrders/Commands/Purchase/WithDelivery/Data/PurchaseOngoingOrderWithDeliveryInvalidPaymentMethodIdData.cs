namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Purchase.WithDelivery.Data;

public class PurchaseOngoingOrderWithDeliveryInvalidPaymentMethodIdData : PurchaseOngoingOrderWithDeliveryData
{
    public PurchaseOngoingOrderWithDeliveryInvalidPaymentMethodIdData()
    {
        Add(string.Empty, 2, "shipment-service-1", "Bulgaria", "Sofia", null, "customcads@gmail.com");
        Add(null!, 5, "shipment-service-2", "Romania", "Bucharest", "+359359359359", null);
    }
}
