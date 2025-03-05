namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Purchase.WithDelivery.Data;

using static OngoingOrdersData;

public class PurchaseOngoingOrderWithDeliveryInvalidCityData : PurchaseOngoingOrderWithDeliveryData
{
    public PurchaseOngoingOrderWithDeliveryInvalidCityData()
    {
        Add("payment-method-id-1", 2, "shipment-service-1", "Bulgaria", string.Empty, null, "customcads@gmail.com");
        Add("payment-method-id-2", 5, "shipment-service-2", "Romania", null!, "+359359359359", null);
    }
}
