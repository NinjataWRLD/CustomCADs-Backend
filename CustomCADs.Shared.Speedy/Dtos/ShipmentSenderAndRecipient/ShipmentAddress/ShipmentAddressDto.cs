namespace CustomCADs.Shared.Speedy.Dtos.ShipmentSenderAndRecipient.ShipmentAddress;

public record ShipmentAddressDto(
    int? CountryId,
    string? PostCode,

    long? SiteId,
    string? SiteType,
    string? SiteName,

    long? ComplexId,
    string? ComplexType,
    string? ComplexName,

    string? StreetId,
    string? StreetType,
    string? StreetName,

    string? StreetNo,
    string? BlockNo,
    string? EntranceNo,
    string? FloorNo,
    string? ApartmentNo,

    long? PoiId,
    string? AddressNote,
    double? X,
    double? Y
);
