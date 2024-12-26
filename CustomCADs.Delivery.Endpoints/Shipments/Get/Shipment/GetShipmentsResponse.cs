using CustomCADs.Delivery.Domain.Shipments.Enums;

namespace CustomCADs.Delivery.Endpoints.Shipments.Get.Shipment;

public record GetShipmentsResponse(
    Guid Id,
    ShipmentStatus ShipmentStatus,
    AddressDto Address,
    Guid BuyerId
);
