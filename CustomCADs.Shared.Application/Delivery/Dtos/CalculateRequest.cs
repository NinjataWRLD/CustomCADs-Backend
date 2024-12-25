namespace CustomCADs.Shared.Application.Delivery.Dtos;

public record CalculateRequest(
    int ParcelCount,
    double TotalWeight,
    string Country,
    string City
);
