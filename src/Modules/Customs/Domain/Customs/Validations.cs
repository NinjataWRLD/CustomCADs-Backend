namespace CustomCADs.Customs.Domain.Customs;

using static CustomConstants;

public static class Validations
{
	public static Custom ValidateName(this Custom custom)
		=> custom
			.ThrowIfNull(
				expression: x => x.Name,
				predicate: string.IsNullOrWhiteSpace
			).ThrowIfInvalidLength(
				expression: x => x.Name,
				length: (NameMinLength, NameMaxLength)
			);

	public static Custom ValidateDescription(this Custom custom)
		=> custom
			.ThrowIfNull(
				expression: x => x.Description,
				predicate: string.IsNullOrWhiteSpace
			).ThrowIfInvalidLength(
				expression: x => x.Description,
				length: (DescriptionMinLength, DescriptionMaxLength)
			);
}
