namespace CustomCADs.Shared.Abstractions.Delivery.Dtos;

public record CalculateRequest(
	double[] Weights,
	string Country,
	string City,
	string Street
);
