using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.ValueObjects;

namespace CustomCADs.Files.Application.Cads;

internal static class Mapper
{
    internal static (string Key, string ContentType, CoordinatesDto CamCoordinates, CoordinatesDto PanCoordinates) ToTuple(this Cad cad)
        => (
            cad.Key,
            cad.ContentType,
            CamCoordinates: cad.CamCoordinates.ToCoordinatesDto(),
            PanCoordinates: cad.PanCoordinates.ToCoordinatesDto()
        );

    internal static Coordinates ToCoordinates(this CoordinatesDto coordinates)
        => new(coordinates.X, coordinates.Y, coordinates.Z);

    internal static CoordinatesDto ToCoordinatesDto(this Coordinates coordinates)
        => new(coordinates.X, coordinates.Y, coordinates.Z);
}
