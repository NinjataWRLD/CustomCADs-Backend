namespace CustomCADs.Speedy.Core.Models.Calculation.Recipient;

public record CalculationRecipientModel(
	CalculationAddressLocationModel? AddressLocation,
	long? ClientId,
	bool? PrivatePerson,
	int? PickupOfficeId,
	string? PickupGeoPUDOIf
);
