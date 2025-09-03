namespace CustomCADs.Catalog.Domain.Tags;

using static TagConstants;

public static class Validations
{
	public static Tag ValidateName(this Tag tag)
		=> tag
			.ThrowIfNull(
				expression: x => x.Name,
				predicate: string.IsNullOrWhiteSpace
			).ThrowIfInvalidLength(
				expression: x => x.Name,
				length: (NameMinLength, NameMaxLength)
			);
}
