using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Cads.Application.Cads;

public static class Mapper
{
    public static Coordinates ToCoordinates(this CoordinatesDto coordinates)
        => new(coordinates.X, coordinates.Y, coordinates.Z);

    public static CoordinatesDto ToCoordinatesDto(this Coordinates coordinates)
        => new(coordinates.X, coordinates.Y, coordinates.Z);
}
