namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Purchase.WithDelivery.Data;

public class PurchaseOngoingOrderWithDeliveryValidData : PurchaseOngoingOrderWithDeliveryData
{
    public PurchaseOngoingOrderWithDeliveryValidData()
    {
        Add("payment-method-id-1", 2, "shipment-service-1", "Bulgaria", "Sofia", null, "customcads@gmail.com");
        Add("payment-method-id-2", 5, "shipment-service-2", "Romania", "Bucharest", "+359359359359", null);
    }
}
