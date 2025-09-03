namespace CustomCADs.Printing.Domain.Customizations;

using static CustomizationConstants;

public static class Validations
{
	public static Customization ValidateScale(this Customization customization)
		=> customization
			.ThrowIfInvalidRange(
				expression: (x) => x.Scale,
				range: (ScaleMin, ScaleMax)
			);

	public static Customization ValidateInfill(this Customization customization)
		=> customization
			.ThrowIfInvalidRange(
				expression: (x) => x.Infill,
				range: (InfillMin, InfillMax)
			);

	public static Customization ValidateVolume(this Customization customization)
		=> customization
			.ThrowIfInvalidRange(
				expression: (x) => x.Volume,
				range: (VolumeMin, VolumeMax)
			);

	public static Customization ValidateColor(this Customization customization)
		=> customization
			.ThrowIfNull(
				expression: (x) => x.Color,
				predicate: string.IsNullOrWhiteSpace
			)
			.ThrowIfPredicateIsFalse(
				expression: (x) => x.Color,
				predicate: Color.IsMatch,
				message: "A {0} must have a proper {1}."
			);
}
