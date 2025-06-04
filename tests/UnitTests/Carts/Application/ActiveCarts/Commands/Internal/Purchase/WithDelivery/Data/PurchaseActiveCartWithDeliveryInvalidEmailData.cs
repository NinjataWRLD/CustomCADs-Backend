namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery.Data;

public class PurchaseActiveCartWithDeliveryInvalidEmailData : PurchaseActiveCartWithDeliveryData
{
	public PurchaseActiveCartWithDeliveryInvalidEmailData()
	{
		Add("payment-method-id-1", "shipment-service-1", "Bulgaria", "Sofia", "Slivnitsa", null, "@gmail.com");
		Add("payment-method-id-1", "shipment-service-1", "Bulgaria", "Sofia", "Slivnitsa", null, "customcads_gmail.com");
		Add("payment-method-id-1", "shipment-service-1", "Bulgaria", "Sofia", "Slivnitsa", null, "customcads@");
		Add("payment-method-id-1", "shipment-service-1", "Bulgaria", "Sofia", "Slivnitsa", null, "customcads@gmail_com");
	}
}
