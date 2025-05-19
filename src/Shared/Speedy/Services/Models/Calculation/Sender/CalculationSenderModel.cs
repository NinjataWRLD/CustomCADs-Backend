using CustomCADs.Shared.Speedy.Services.Models.Calculation.Recipient;

namespace CustomCADs.Shared.Speedy.Services.Models.Calculation.Sender;

public record CalculationSenderModel(
	CalculationAddressLocationModel? AddressLocation,
	long? ClientId,
	bool? PrivatePerson,
	int? DropoffOfficeId,
	string? DropoffGeoPUDOId
);
