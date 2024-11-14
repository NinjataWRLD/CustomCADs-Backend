using CustomCADs.Shared.Speedy.ShipmentService.Dtos;

namespace CustomCADs.Shared.Speedy.ShipmentService.CreateShipment;

public record CreateShipmentResponse(
    string Id,
    CreatedParcel[] Parcels,
    ShipmentPrice Price,
    string PickupDate,
    string DeliveryDeadline,
    Error? Error
);