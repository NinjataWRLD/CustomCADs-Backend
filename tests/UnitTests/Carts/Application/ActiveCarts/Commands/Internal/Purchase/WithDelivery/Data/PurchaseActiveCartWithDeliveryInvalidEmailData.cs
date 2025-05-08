namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery.Data;

using CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;
using static ActiveCartsData;

public class PurchaseActiveCartWithDeliveryInvalidEmailData : PurchaseActiveCartWithDeliveryData
{
    public PurchaseActiveCartWithDeliveryInvalidEmailData()
    {
        Add("payment-method-id-1", ValidBuyerId1, "shipment-service-1", "Bulgaria", "Sofia", "Flora", null, "@gmail.com");
        Add("payment-method-id-1", ValidBuyerId1, "shipment-service-1", "Bulgaria", "Sofia", "Flora", null, "customcads_gmail.com");
        Add("payment-method-id-1", ValidBuyerId1, "shipment-service-1", "Bulgaria", "Sofia", "Flora", null, "customcads@");
        Add("payment-method-id-1", ValidBuyerId1, "shipment-service-1", "Bulgaria", "Sofia", "Flora", null, "customcads@gmail_com");
    }
}
