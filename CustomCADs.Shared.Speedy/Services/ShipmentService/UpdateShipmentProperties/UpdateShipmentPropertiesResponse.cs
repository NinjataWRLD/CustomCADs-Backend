namespace CustomCADs.Shared.Speedy.Services.ShipmentService.UpdateShipmentProperties;

using Dtos.Errors;
using Dtos.ShipmentParcels;
using Dtos.ShipmentPrice;

public record UpdateShipmentPropertiesResponse(
    // Copied from UpdateShipmentResponse
    string Id,
    CreatedShipmentParcelDto[] Parcels,
    ShipmentPriceDto Price,
    string PickupDate,
    string DeliveryDeadline,
    ErrorDto? Error
);
