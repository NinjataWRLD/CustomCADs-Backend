namespace CustomCADs.Speedy.Http.Dtos.SpecialDeliveryRequirements;

/// <param name="Id">
///     Requirement id.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Text">
///     Requirement text.
///     <br />
///     Required: Yes.
/// </param>
internal record RequirementDto(
	int Id,
	string Text
);
