namespace CustomCADs.Shared.Speedy.API.Dtos.CalculationRecipient;

using CalculationAddressLocation;

public record CalculationRecipientDto(
	CalculationAddressLocationDto? AddressLocation,
	long? ClientId,
	bool? PrivatePerson,
	int? PickupOfficeId,
	string? PickupGeoPUDOId
);
