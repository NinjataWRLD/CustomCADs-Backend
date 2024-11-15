namespace CustomCADs.Shared.Speedy.Dtos.SpecialDeliveryRequirements;

public record SpecialDeliveryRequirementsDto(
    bool RequiredForAllShipments,
    RequirementDto[] Requirements
);
