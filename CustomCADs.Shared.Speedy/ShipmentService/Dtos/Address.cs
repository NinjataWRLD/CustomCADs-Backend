namespace CustomCADs.Shared.Speedy.ShipmentService.Dtos;

public record Address(
    string StreetType,
    string StreetName,
    string StreetNo,
    string SiteName,
    string SiteType,
    string? PostCode,
    string? ComplexName,
    string? ComplexType,
    string? EntranceNo,
    string? FloorNo,
    string? ApartmentNo
);
