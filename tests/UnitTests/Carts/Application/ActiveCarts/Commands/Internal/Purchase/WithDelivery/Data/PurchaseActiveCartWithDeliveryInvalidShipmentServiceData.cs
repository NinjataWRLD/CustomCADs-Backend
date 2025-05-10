namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery.Data;

using CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;
using static ActiveCartsData;

public class PurchaseActiveCartWithDeliveryInvalidShipmentServiceData : PurchaseActiveCartWithDeliveryData
{
    public PurchaseActiveCartWithDeliveryInvalidShipmentServiceData()
    {
        Add("payment-method-id-1", ValidBuyerId, string.Empty, "Bulgaria", "Sofia", null, "customcads@gmail.com");
        Add("payment-method-id-2", ValidBuyerId, null!, "Romania", "Bucharest", "+359359359359", null);
    }
}
