namespace CustomCADs.Shared.Application.Dtos.Delivery;

public record ShipmentInfoDto(
	int Count,
	double Weight,
	string Recipient
);
