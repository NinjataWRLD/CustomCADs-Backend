namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery.Data;

public class PurchaseActiveCartWithDeliveryValidData : PurchaseActiveCartWithDeliveryData
{
	public PurchaseActiveCartWithDeliveryValidData()
	{
		Add("payment-method-id-1", "shipment-service-1", "Bulgaria", "Sofia", "Slivnitsa", null, "customcads@gmail.com");
		Add("payment-method-id-2", "shipment-service-2", "Romania", "Bucharest", "Brailles", "+359359359359", null);
	}
}
