using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.ValueObjects;

namespace CustomCADs.Files.Application.Cads;

internal static class Mapper
{
	internal static (string Key, string ContentType, CoordinatesDto CamCoordinates, CoordinatesDto PanCoordinates) ToTuple(this Cad cad)
		=> (
			Key: cad.Key,
			ContentType: cad.ContentType,
			CamCoordinates: cad.CamCoordinates.ToDto(),
			PanCoordinates: cad.PanCoordinates.ToDto()
		);

	internal static Coordinates ToValueObject(this CoordinatesDto coordinates)
		=> new(coordinates.X, coordinates.Y, coordinates.Z);

	internal static CoordinatesDto ToDto(this Coordinates coordinates)
		=> new(coordinates.X, coordinates.Y, coordinates.Z);
}
