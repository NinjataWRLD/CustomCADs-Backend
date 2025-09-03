namespace CustomCADs.Printing.Domain.Materials;

using static MaterialConstants;

public static class Validations
{
	public static Material ValidateName(this Material material)
		=> material
			.ThrowIfNull(
				expression: (x) => x.Name,
				predicate: string.IsNullOrWhiteSpace
			)
			.ThrowIfInvalidLength(
				expression: (x) => x.Name,
				length: (NameMinLength, NameMaxLength)
			);

	public static Material ValidateDensity(this Material material)
		=> material
			.ThrowIfInvalidRange(
				expression: (x) => x.Density,
				range: (DensityMin, DensityMax)
			);

	public static Material ValidateCost(this Material material)
		=> material
			.ThrowIfInvalidRange(
				expression: (x) => x.Cost,
				range: (CostMin, CostMax)
			);
}
