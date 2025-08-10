namespace CustomCADs.Speedy.API.Dtos.SpecialDeliveryRequirements;

public record SpecialDeliveryRequirementsDto(
	bool RequiredForAllShipments,
	RequirementDto[] Requirements
);
