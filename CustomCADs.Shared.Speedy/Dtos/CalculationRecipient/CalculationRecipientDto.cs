namespace CustomCADs.Shared.Speedy.Dtos.CalculationRecipient;

using CalculationAddressLocation;

public record CalculationRecipientDto(
    CalculationAddressLocationDto? AddressLocation,
    long? ClientId,
    bool? PrivatePerson,
    int? PickupOfficeId,
    string? PickupGeoPUDOIf
);
