using CustomCADs.Shared.Core.Domain.ValueObjects.Deliveries.Digital;

namespace CustomCADs.Shared.Core.Dtos;

public record CoordinatesDto(int X = 0, int Y = 0, int Z = 0)
{
    public CoordinatesDto(Coordinates coordinates) : this(
        coordinates.X,
        coordinates.Y,
        coordinates.Z
    )
    { }
}