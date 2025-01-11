namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Purchase.WithDelivery.Data;

using static ActiveCartsData;

public class PurchaseActiveCartWithDeliveryInvalidEmailData : PurchaseActiveCartWithDeliveryData
{
    public PurchaseActiveCartWithDeliveryInvalidEmailData()
    {
        Add("payment-method-id-1", ValidBuyerId1, "shipment-service-1", "Bulgaria", "Sofia", null, "@gmail.com");
        Add("payment-method-id-1", ValidBuyerId1, "shipment-service-1", "Bulgaria", "Sofia", null, "customcads_gmail.com");
        Add("payment-method-id-1", ValidBuyerId1, "shipment-service-1", "Bulgaria", "Sofia", null, "customcads@");
        Add("payment-method-id-1", ValidBuyerId1, "shipment-service-1", "Bulgaria", "Sofia", null, "customcads@gmail_com");
    }
}
