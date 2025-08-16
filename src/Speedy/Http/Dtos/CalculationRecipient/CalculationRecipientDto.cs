namespace CustomCADs.Speedy.Http.Dtos.CalculationRecipient;

using CalculationAddressLocation;

internal record CalculationRecipientDto(
	CalculationAddressLocationDto? AddressLocation,
	long? ClientId,
	bool? PrivatePerson,
	int? PickupOfficeId,
	string? PickupGeoPUDOId
);
