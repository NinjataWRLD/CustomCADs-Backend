namespace CustomCADs.Shared.Speedy.Services.ShipmentService.SecondaryShipment;

using Dtos.Errors;
using Dtos.Shipment.Secondary;

public record SecondaryShipmentResponse(
    SecondaryShipmentDto[] Shipments,
    ErrorDto? Error
);
