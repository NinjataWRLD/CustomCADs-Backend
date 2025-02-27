namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Purchase.WithDelivery.Data;

using static ActiveCartsData;

public class PurchaseActiveCartWithDeliveryInvalidPaymentMethodIdData : PurchaseActiveCartWithDeliveryData
{
    public PurchaseActiveCartWithDeliveryInvalidPaymentMethodIdData()
    {
        Add(string.Empty, ValidBuyerId1, "shipment-service-1", "Bulgaria", "Sofia", null, "customcads@gmail.com");
        Add(null!, ValidBuyerId2, "shipment-service-2", "Romania", "Bucharest", "+359359359359", null);
    }
}
