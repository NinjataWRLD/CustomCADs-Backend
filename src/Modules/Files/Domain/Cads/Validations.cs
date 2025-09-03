namespace CustomCADs.Files.Domain.Cads;

using static CadConstants;

public static class Validations
{
	private const string BaseMessage = "A/An {0}'s {1} must be";

	public static Cad ValidateKey(this Cad cad)
		=> cad
			.ThrowIfNull(
				expression: x => x.Key,
				predicate: string.IsNullOrWhiteSpace
			);

	public static Cad ValidateContentType(this Cad cad)
		=> cad
			.ThrowIfNull(
				expression: x => x.ContentType,
				predicate: string.IsNullOrWhiteSpace
			);

	public static Cad ValidateVolume(this Cad cad)
		=> cad
			.ThrowIfInvalidRange(
				expression: x => x.Volume,
				range: (VolumeMin, VolumeMax),
				inclusive: true
			);

	public static Cad ValidateCamCoordinates(this Cad cad)
		=> cad
			.ThrowIfPredicateIsFalse(
				expression: x => x.CamCoordinates,
				predicate: AreCoordsValid,
				message: $"{BaseMessage} more than {VolumeMin} and less than {VolumeMax}."
			);

	public static Cad ValidatePanCoordinates(this Cad cad)
		=> cad
			.ThrowIfPredicateIsFalse(
				expression: x => x.PanCoordinates,
				predicate: AreCoordsValid,
				message: $"{BaseMessage} more than {VolumeMin} and less than {VolumeMax}."
			);

	private static bool AreCoordsValid(ValueObjects.Coordinates coordinates)
	{
		static bool IsCoordValid(decimal coord)
			=> coord > Coordinates.CoordMin &&
			   coord < Coordinates.CoordMax;

		return
			IsCoordValid(coordinates.X) &&
			IsCoordValid(coordinates.Y) &&
			IsCoordValid(coordinates.Z);
	}
}
