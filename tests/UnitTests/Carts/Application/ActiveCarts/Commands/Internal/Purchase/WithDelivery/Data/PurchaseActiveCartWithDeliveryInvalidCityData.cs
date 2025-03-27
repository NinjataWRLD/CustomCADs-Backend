namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery.Data;

using CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;
using static ActiveCartsData;

public class PurchaseActiveCartWithDeliveryInvalidCityData : PurchaseActiveCartWithDeliveryData
{
    public PurchaseActiveCartWithDeliveryInvalidCityData()
    {
        Add("payment-method-id-1", ValidBuyerId1, "shipment-service-1", "Bulgaria", string.Empty, null, "customcads@gmail.com");
        Add("payment-method-id-2", ValidBuyerId2, "shipment-service-2", "Romania", null!, "+359359359359", null);
    }
}
