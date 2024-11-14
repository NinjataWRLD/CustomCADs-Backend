using CustomCADs.Shared.Speedy.ShipmentService.CreateShipment.Response.CreatedShipmentParcel;
using CustomCADs.Shared.Speedy.ShipmentService.CreateShipment.Response.ShipmentPrice;

namespace CustomCADs.Shared.Speedy.ShipmentService.CreateShipment.Response;

public record CreateShipmentResponse(
    string Id,
    CreatedParcel[] Parcels,
    Price Price,
    string PickupDate,
    string DeliveryDeadline,
    Error Erorr
);