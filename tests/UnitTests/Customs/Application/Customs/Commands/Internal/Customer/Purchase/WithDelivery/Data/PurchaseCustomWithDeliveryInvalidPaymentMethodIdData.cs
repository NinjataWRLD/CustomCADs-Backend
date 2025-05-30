namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Purchase.WithDelivery.Data;

public class PurchaseCustomWithDeliveryInvalidPaymentMethodIdData : PurchaseCustomWithDeliveryData
{
    public PurchaseCustomWithDeliveryInvalidPaymentMethodIdData()
    {
        Add(string.Empty, 2, "shipment-service-1", "Bulgaria", "Sofia", "Flora", null, "customcads@gmail.com");
        Add(null!, 5, "shipment-service-2", "Romania", "Bucharest", "Brailles", "+359359359359", null);
    }
}
