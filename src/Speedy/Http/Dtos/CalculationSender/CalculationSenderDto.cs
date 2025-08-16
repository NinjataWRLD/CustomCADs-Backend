namespace CustomCADs.Speedy.Http.Dtos.CalculationSender;

using CalculationAddressLocation;

internal record CalculationSenderDto(
	CalculationAddressLocationDto? AddressLocation,
	long? ClientId,
	bool? PrivatePerson,
	int? DropoffOfficeId,
	string? DropoffGeoPUDOId
);
