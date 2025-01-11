namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Purchase.WithDelivery.Data;

using static ActiveCartsData;

public class PurchaseActiveCartWithDeliveryInvalidPhoneData : PurchaseActiveCartWithDeliveryData
{
    public PurchaseActiveCartWithDeliveryInvalidPhoneData()
    {
        Add("payment-method-id-1", ValidBuyerId1, "shipment-service-1", "Bulgaria", "Sofia", "0359359359", "customcads@gmail.com");
        Add("payment-method-id-2", ValidBuyerId2, "shipment-service-2", "Romania", "Bucharest", "+359 359 359 359", null);
    }
}
