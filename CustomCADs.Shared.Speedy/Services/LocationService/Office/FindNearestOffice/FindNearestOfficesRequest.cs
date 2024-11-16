namespace CustomCADs.Shared.Speedy.Services.LocationService.Office.FindNearestOffice;

using Dtos.ShipmentSenderAndRecipient.ShipmentAddress;
using Enums;
using Speedy.Enums;

public record FindNearestOfficesRequest(
    string UserName,
    string Password,
    ShipmentAddressDto Address,
    string? Location,
    long? ClientSystemId,
    int? Distance,
    int? Limit,
    OfficeType? OfficeType,
    OfficeFeature[]? OfficeFeatures
);