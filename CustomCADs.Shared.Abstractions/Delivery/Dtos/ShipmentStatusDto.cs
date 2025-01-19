namespace CustomCADs.Shared.Abstractions.Delivery.Dtos;

public record ShipmentStatusDto(
    DateTime DateTime,
    string? Place,
    string Message
);
