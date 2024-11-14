namespace CustomCADs.Shared.Speedy.ShipmentService.CreateShipment.Request.ShipmentRecipient;

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
