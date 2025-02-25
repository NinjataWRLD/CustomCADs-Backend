namespace CustomCADs.Delivery.Endpoints.Shipments.Get.Shipment;

public record GetShipmentsResponse(
    Guid Id,
    AddressDto Address,
    string BuyerName
);
