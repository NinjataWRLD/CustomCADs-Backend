namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Internal.Purchase.WithDelivery.Data;

public class PurchaseOngoingOrderWithDeliveryInvalidCountryData : PurchaseOngoingOrderWithDeliveryData
{
    public PurchaseOngoingOrderWithDeliveryInvalidCountryData()
    {
        Add("payment-method-id-1", 2, "shipment-service-1", string.Empty, "Sofia", null, "customcads@gmail.com");
        Add("payment-method-id-2", 5, "shipment-service-2", null!, "Bucharest", "+359359359359", null);
    }
}
