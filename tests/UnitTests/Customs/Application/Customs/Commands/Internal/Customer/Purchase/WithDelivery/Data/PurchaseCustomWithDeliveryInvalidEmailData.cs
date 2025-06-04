namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Purchase.WithDelivery.Data;

public class PurchaseCustomWithDeliveryInvalidEmailData : PurchaseCustomWithDeliveryData
{
	public PurchaseCustomWithDeliveryInvalidEmailData()
	{
		Add("payment-method-id-1", 2, "shipment-service-1", "Bulgaria", "Sofia", "Flora", null, "@gmail.com");
		Add("payment-method-id-1", 5, "shipment-service-1", "Bulgaria", "Sofia", "Flora", null, "customcads_gmail.com");
		Add("payment-method-id-1", 2, "shipment-service-1", "Bulgaria", "Sofia", "Flora", null, "customcads@");
		Add("payment-method-id-1", 5, "shipment-service-1", "Bulgaria", "Sofia", "Flora", null, "customcads@gmail_com");
	}
}
