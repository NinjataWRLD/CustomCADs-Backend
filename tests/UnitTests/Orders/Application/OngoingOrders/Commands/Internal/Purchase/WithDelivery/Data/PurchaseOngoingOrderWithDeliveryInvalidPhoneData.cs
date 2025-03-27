namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Internal.Purchase.WithDelivery.Data;

public class PurchaseOngoingOrderWithDeliveryInvalidPhoneData : PurchaseOngoingOrderWithDeliveryData
{
    public PurchaseOngoingOrderWithDeliveryInvalidPhoneData()
    {
        Add("payment-method-id-1", 2, "shipment-service-1", "Bulgaria", "Sofia", "0359359359", "customcads@gmail.com");
        Add("payment-method-id-2", 5, "shipment-service-2", "Romania", "Bucharest", "+359 359 359 359", null);
    }
}
