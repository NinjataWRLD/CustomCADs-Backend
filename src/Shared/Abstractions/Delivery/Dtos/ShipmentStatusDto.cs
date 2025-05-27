namespace CustomCADs.Shared.Abstractions.Delivery.Dtos;

public record ShipmentStatusDto(
	DateTimeOffset DateTime,
	string? Place,
	string Message
);
