namespace CustomCADs.Speedy.Http.Dtos.SpecialDeliveryRequirements;

/// <param name="RequiredForAllShipments">
///     Flag indicating if requirements are needed for all shipments.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Requirements">
///     List of requirements for special delivery.
///     <br />
///     Required: Yes. See RequirementDto for details.
/// </param>
internal record SpecialDeliveryRequirementsDto(
	bool RequiredForAllShipments,
	RequirementDto[] Requirements
);
