using CustomCADs.Cads.Domain.Cads;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.ValueObjects;

namespace CustomCADs.Cads.Application.Cads;

public static class Mapper
{
    public static (string Key, string ContentType, CoordinatesDto CamCoordinates, CoordinatesDto PanCoordinates) ToTuple(this Cad cad)
        => (
            Key: cad.Key,
            ContentType: cad.ContentType,
            CamCoordinates: cad.CamCoordinates.ToCoordinatesDto(),
            PanCoordinates: cad.PanCoordinates.ToCoordinatesDto()
        );

    public static Coordinates ToCoordinates(this CoordinatesDto coordinates)
        => new(coordinates.X, coordinates.Y, coordinates.Z);

    public static CoordinatesDto ToCoordinatesDto(this Coordinates coordinates)
        => new(coordinates.X, coordinates.Y, coordinates.Z);
}
