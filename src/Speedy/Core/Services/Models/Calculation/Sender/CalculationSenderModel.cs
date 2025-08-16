using CustomCADs.Speedy.Core.Services.Models.Calculation.Recipient;

namespace CustomCADs.Speedy.Core.Services.Models.Calculation.Sender;

public record CalculationSenderModel(
	CalculationAddressLocationModel? AddressLocation,
	long? ClientId,
	bool? PrivatePerson,
	int? DropoffOfficeId,
	string? DropoffGeoPUDOId
);
