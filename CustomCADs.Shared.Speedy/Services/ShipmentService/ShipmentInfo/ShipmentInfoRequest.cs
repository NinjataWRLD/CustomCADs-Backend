namespace CustomCADs.Shared.Speedy.Services.ShipmentService.ShipmentInfo;

public record ShipmentInfoRequest(
    string UserName,
    string Password,
    string[] ShipmentIds,
    string? Language,
    string? ClientSystemId
);
