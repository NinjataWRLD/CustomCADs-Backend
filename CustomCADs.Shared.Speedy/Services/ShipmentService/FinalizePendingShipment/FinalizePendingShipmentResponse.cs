namespace CustomCADs.Shared.Speedy.Services.ShipmentService.FinalizePendingShipment;

using Dtos.Errors;
using Dtos.ShipmentParcels;
using Dtos.ShipmentPrice;

public record FinalizePendingShipmentResponse(
    // Copied from CreateShipmentResponse
    string Id,
    CreatedShipmentParcelDto[] Parcels,
    ShipmentPriceDto Price,
    string PickupDate,
    string DeliveryDeadline,
    ErrorDto? Error
);
