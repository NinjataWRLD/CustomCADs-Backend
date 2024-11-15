using CustomCADs.Shared.Speedy.Dtos.Errors;
using CustomCADs.Shared.Speedy.Dtos.ShipmentParcels;
using CustomCADs.Shared.Speedy.Dtos.ShipmentPrice;

namespace CustomCADs.Shared.Speedy.Services.ShipmentService.UpdateShipment;

public record UpdateShipmentResponse(
    // Copied from CreateShipmentResponse
    string Id,
    CreatedShipmentParcelDto[] Parcels,
    ShipmentPriceDto Price,
    string PickupDate,
    string DeliveryDeadline,
    ErrorDto? Error
);
