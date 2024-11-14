namespace CustomCADs.Shared.Speedy.ShipmentService.Dtos;

public record Error(
    string Message,
    string Id,
    int Code,
    string? Context,
    string? Component
);
