namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery.Data;

public class PurchaseActiveCartWithDeliveryInvalidCountryData : PurchaseActiveCartWithDeliveryData
{
	public PurchaseActiveCartWithDeliveryInvalidCountryData()
	{
		Add("payment-method-id-1", "shipment-service-1", string.Empty, "Sofia", "Slivnitsa", null, "customcads@gmail.com");
		Add("payment-method-id-2", "shipment-service-2", null!, "Bucharest", "Brailles", "+359359359359", null);
	}
}
