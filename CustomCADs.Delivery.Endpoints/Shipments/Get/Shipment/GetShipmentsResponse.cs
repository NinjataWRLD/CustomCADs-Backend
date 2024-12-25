using CustomCADs.Delivery.Domain.Shipments.Enums;
using CustomCADs.Delivery.Endpoints.Common.Dto;

namespace CustomCADs.Delivery.Endpoints.Shipments.Get.Shipment;

public record GetShipmentsResponse(
    Guid Id,
    ShipmentStatus ShipmentStatus,
    AddressDto Address,
    Guid BuyerId
);
