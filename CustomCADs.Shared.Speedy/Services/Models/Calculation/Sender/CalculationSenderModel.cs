using CustomCADs.Shared.Speedy.Models.Calculation.Recipient;

namespace CustomCADs.Shared.Speedy.Models.Calculation.Sender;

public record CalculationSenderModel(
    CalculationAddressLocationModel? AddressLocation,
    long? ClientId,
    bool? PrivatePerson,
    int? DropoffOfficeId,
    string? DropoffGeoPUDOId
);
