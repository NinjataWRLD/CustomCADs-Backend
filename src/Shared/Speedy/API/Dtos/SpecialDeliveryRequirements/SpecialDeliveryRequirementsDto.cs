namespace CustomCADs.Shared.Speedy.API.Dtos.SpecialDeliveryRequirements;

public record SpecialDeliveryRequirementsDto(
    bool RequiredForAllShipments,
    RequirementDto[] Requirements
);
