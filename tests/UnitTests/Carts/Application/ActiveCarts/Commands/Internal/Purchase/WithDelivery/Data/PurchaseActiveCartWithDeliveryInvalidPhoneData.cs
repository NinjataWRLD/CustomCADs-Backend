namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery.Data;

using CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;
using static ActiveCartsData;

public class PurchaseActiveCartWithDeliveryInvalidPhoneData : PurchaseActiveCartWithDeliveryData
{
    public PurchaseActiveCartWithDeliveryInvalidPhoneData()
    {
        Add("payment-method-id-1", ValidBuyerId, "shipment-service-1", "Bulgaria", "Sofia", "0359359359", "customcads@gmail.com");
        Add("payment-method-id-2", ValidBuyerId, "shipment-service-2", "Romania", "Bucharest", "+359 359 359 359", null);
    }
}
