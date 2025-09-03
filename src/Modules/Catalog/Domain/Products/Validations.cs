namespace CustomCADs.Catalog.Domain.Products;

using static ProductConstants;

public static class Validations
{
	public static Product ValidateName(this Product product)
		=> product
			.ThrowIfNull(
				expression: x => x.Name,
				predicate: string.IsNullOrWhiteSpace
			)
			.ThrowIfInvalidLength(
				expression: x => x.Name,
				length: (NameMinLength, NameMaxLength)
			);

	public static Product ValidateDescription(this Product product)
		=> product
			.ThrowIfNull(
				expression: x => x.Description,
				predicate: string.IsNullOrWhiteSpace
			)
			.ThrowIfInvalidLength(
				expression: x => x.Description,
				length: (DescriptionMinLength, DescriptionMaxLength)
			);

	public static Product ValidatePrice(this Product product)
		=> product
			.ThrowIfInvalidRange(
				expression: x => x.Price,
				range: (PriceMin, PriceMax)
			);
}
