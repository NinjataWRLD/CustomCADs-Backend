namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery.Data;

using CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;
using static ActiveCartsData;

public class PurchaseActiveCartWithDeliveryInvalidPaymentMethodIdData : PurchaseActiveCartWithDeliveryData
{
    public PurchaseActiveCartWithDeliveryInvalidPaymentMethodIdData()
    {
        Add(string.Empty, ValidBuyerId, "shipment-service-1", "Bulgaria", "Sofia", null, "customcads@gmail.com");
        Add(null!, ValidBuyerId, "shipment-service-2", "Romania", "Bucharest", "+359359359359", null);
    }
}
