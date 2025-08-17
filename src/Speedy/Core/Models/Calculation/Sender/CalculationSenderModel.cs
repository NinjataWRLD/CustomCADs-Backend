using CustomCADs.Speedy.Core.Models.Calculation.Recipient;

namespace CustomCADs.Speedy.Core.Models.Calculation.Sender;

public record CalculationSenderModel(
	CalculationAddressLocationModel? AddressLocation,
	long? ClientId,
	bool? PrivatePerson,
	int? DropoffOfficeId,
	string? DropoffGeoPUDOId
);
