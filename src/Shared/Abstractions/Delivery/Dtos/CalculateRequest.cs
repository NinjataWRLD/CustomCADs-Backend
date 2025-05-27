namespace CustomCADs.Shared.Abstractions.Delivery.Dtos;

public record CalculateRequest(
	int ParcelCount,
	double TotalWeight,
	string Country,
	string City
);
