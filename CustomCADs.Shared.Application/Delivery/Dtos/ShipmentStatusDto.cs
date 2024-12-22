namespace CustomCADs.Shared.Application.Delivery.Dtos;

public record ShipmentStatusDto(
    DateTime DateTime,
    string? Place,
    string Message
);
