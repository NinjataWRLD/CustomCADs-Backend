namespace CustomCADs.Shared.Speedy.API.Endpoints.ShipmentEndpoints.CreateShipment;

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