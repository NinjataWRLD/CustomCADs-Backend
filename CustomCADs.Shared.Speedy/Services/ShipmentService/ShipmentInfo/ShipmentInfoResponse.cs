namespace CustomCADs.Shared.Speedy.Services.ShipmentService.ShipmentInfo;

using Dtos.Errors;
using Dtos.Shipment;

public record ShipmentInfoResponse(
    ShipmentDto[] Shipments,
    ErrorDto? Error
);
