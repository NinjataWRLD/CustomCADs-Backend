namespace CustomCADs.Shared.Abstractions.Delivery.Dtos;

public record ShipRequestDto(
    string Country,
    string City,
    string? Phone,
    string? Email,
    string Name,
    string Service,
    string Package,
    string Contents,
    int ParcelCount,
    double TotalWeight
);
