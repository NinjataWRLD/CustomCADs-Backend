namespace CustomCADs.Shared.Speedy.Services.ShipmentService.CreateShipment;

using Dtos.Errors;
using Dtos.ShipmentParcels;
using Dtos.ShipmentPrice;

public record CreateShipmentResponse(
    string Id,
    CreatedShipmentParcelDto[] Parcels,
    ShipmentPriceDto Price,
    string PickupDate,
    string DeliveryDeadline,
    ErrorDto? Error
);