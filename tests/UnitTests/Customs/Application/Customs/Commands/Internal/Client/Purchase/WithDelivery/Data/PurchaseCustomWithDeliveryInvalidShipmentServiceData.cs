namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Client.Purchase.WithDelivery.Data;

public class PurchaseCustomWithDeliveryInvalidShipmentServiceData : PurchaseCustomWithDeliveryData
{
    public PurchaseCustomWithDeliveryInvalidShipmentServiceData()
    {
        Add("payment-method-id-1", 2, string.Empty, "Bulgaria", "Sofia", null, "customcads@gmail.com");
        Add("payment-method-id-2", 5, null!, "Romania", "Bucharest", "+359359359359", null);
    }
}
