namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Client.Purchase.WithDelivery.Data;

public class PurchaseCustomWithDeliveryInvalidEmailData : PurchaseCustomWithDeliveryData
{
    public PurchaseCustomWithDeliveryInvalidEmailData()
    {
        Add("payment-method-id-1", 2, "shipment-service-1", "Bulgaria", "Sofia", null, "@gmail.com");
        Add("payment-method-id-1", 5, "shipment-service-1", "Bulgaria", "Sofia", null, "customcads_gmail.com");
        Add("payment-method-id-1", 2, "shipment-service-1", "Bulgaria", "Sofia", null, "customcads@");
        Add("payment-method-id-1", 5, "shipment-service-1", "Bulgaria", "Sofia", null, "customcads@gmail_com");
    }
}
