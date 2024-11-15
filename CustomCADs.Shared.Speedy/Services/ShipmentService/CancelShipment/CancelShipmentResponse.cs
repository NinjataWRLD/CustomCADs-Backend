namespace CustomCADs.Shared.Speedy.Services.ShipmentService.CancelShipment;

using Dtos.Errors;

public record CancelShipmentResponse(
    ErrorDto? Error
);
