namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery.Data;

public class PurchaseActiveCartWithDeliveryInvalidPaymentMethodIdData : PurchaseActiveCartWithDeliveryData
{
	public PurchaseActiveCartWithDeliveryInvalidPaymentMethodIdData()
	{
		Add(string.Empty, "shipment-service-1", "Bulgaria", "Sofia", "Slivnitsa", null, "customcads@gmail.com");
		Add(null!, "shipment-service-2", "Romania", "Bucharest", "Brailles", "+359359359359", null);
	}
}
