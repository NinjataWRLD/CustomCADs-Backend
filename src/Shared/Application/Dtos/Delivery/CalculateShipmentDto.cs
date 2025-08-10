namespace CustomCADs.Shared.Application.Dtos.Delivery;

public record CalculateShipmentDto(
	double Total,
	string Currency,
	string Service,
	DateOnly PickupDate,
	DateTimeOffset DeliveryDeadline
);
