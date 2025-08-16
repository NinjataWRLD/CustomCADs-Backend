namespace CustomCADs.Speedy.Http.Dtos.SpecialDeliveryRequirements;

internal record SpecialDeliveryRequirementsDto(
	bool RequiredForAllShipments,
	RequirementDto[] Requirements
);
