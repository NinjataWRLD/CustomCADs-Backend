using CustomCADs.Shared.Speedy.ShipmentService.Dtos;

namespace CustomCADs.Shared.Speedy.ShipmentService.ShipmentInfo;

public record ShipmentInfoResponse(
    Shipment[] Shipments,
    Error? Error
);
