namespace CustomCADs.Catalog.Domain.Categories;

using static CategoryConstants;

public static class Validations
{
	public static Category ValidateName(this Category category)
		=> category
			.ThrowIfNull(
				expression: x => x.Name,
				predicate: string.IsNullOrWhiteSpace
			).ThrowIfInvalidLength(
				expression: x => x.Name,
				length: (NameMinLength, NameMaxLength)
			);

	public static Category ValidateDescription(this Category category)
		=> category
			.ThrowIfNull(
				expression: x => x.Description,
				predicate: string.IsNullOrWhiteSpace
			).ThrowIfInvalidLength(
				expression: x => x.Description,
				length: (DescriptionMinLength, DescriptionMaxLength)
			);
}

