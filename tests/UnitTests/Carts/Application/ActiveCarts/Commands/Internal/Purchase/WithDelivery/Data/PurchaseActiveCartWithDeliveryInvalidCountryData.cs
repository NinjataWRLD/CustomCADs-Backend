namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery.Data;

using CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;
using static ActiveCartsData;

public class PurchaseActiveCartWithDeliveryInvalidCountryData : PurchaseActiveCartWithDeliveryData
{
    public PurchaseActiveCartWithDeliveryInvalidCountryData()
    {
        Add("payment-method-id-1", ValidBuyerId, "shipment-service-1", string.Empty, "Sofia", null, "customcads@gmail.com");
        Add("payment-method-id-2", ValidBuyerId, "shipment-service-2", null!, "Bucharest", "+359359359359", null);
    }
}
