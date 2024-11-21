namespace CustomCADs.Shared.Speedy.Models.Calculation.Recipient;

public record CalculationRecipientModel(
    CalculationAddressLocationModel? AddressLocation,
    long? ClientId,
    bool? PrivatePerson,
    int? PickupOfficeId,
    string? PickupGeoPUDOIf
);
