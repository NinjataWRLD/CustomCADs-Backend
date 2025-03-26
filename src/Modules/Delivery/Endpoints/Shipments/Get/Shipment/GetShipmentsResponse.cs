namespace CustomCADs.Delivery.Endpoints.Shipments.Get.Shipment;

public record GetShipmentsResponse(
    Guid Id,
    AddressResponse Address,
    string BuyerName
);
