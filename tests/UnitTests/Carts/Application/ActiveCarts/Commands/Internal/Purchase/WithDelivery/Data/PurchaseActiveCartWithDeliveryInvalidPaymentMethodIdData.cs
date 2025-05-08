namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery.Data;

using CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;
using static ActiveCartsData;

public class PurchaseActiveCartWithDeliveryInvalidPaymentMethodIdData : PurchaseActiveCartWithDeliveryData
{
    public PurchaseActiveCartWithDeliveryInvalidPaymentMethodIdData()
    {
        Add(string.Empty, ValidBuyerId1, "shipment-service-1", "Bulgaria", "Sofia", "Slivnitsa", null, "customcads@gmail.com");
        Add(null!, ValidBuyerId2, "shipment-service-2", "Romania", "Bucharest", "Brailles", "+359359359359", null);
    }
}
