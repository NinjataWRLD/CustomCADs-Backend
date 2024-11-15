namespace CustomCADs.Shared.Speedy.Dtos.CalculationSender;

using CalculationAddressLocation;

public record CalculationSenderDto(
    CalculationAddressLocationDto? AddressLocation,
    long? ClientId,
    bool? PrivatePerson,
    int? DropoffOfficeId,
    string? DropoffGeoPUDOId
);
