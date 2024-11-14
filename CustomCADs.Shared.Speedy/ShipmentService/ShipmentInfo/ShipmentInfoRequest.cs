namespace CustomCADs.Shared.Speedy.ShipmentService.ShipmentInfo;

public record ShipmentInfoRequest(
    string UserName,
    string Password,
    string[] ShipmentIds,
    string? Language,
    string? ClientSystemId
);
