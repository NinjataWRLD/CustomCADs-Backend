namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery.Data;

public class PurchaseActiveCartWithDeliveryInvalidShipmentServiceData : PurchaseActiveCartWithDeliveryData
{
    public PurchaseActiveCartWithDeliveryInvalidShipmentServiceData()
    {
        Add("payment-method-id-1", string.Empty, "Bulgaria", "Sofia", null, "customcads@gmail.com");
        Add("payment-method-id-2", null!, "Romania", "Bucharest", "+359359359359", null);
    }
}
