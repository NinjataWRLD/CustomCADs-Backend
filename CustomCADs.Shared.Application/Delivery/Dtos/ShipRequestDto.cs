namespace CustomCADs.Shared.Application.Delivery.Dtos;

public record ShipRequestDto(
    string Country,
    string City,
    string? Phone,
    string? Email,
    string Name,
    string Package,
    string Contents,
    int ParcelCount,
    double TotalWeight
);
