using CustomCADs.Shared.Core.ValueObjects;

namespace CustomCADs.Catalog.Endpoints.Products;

public record CoordinatesDto(int X = 0, int Y = 0, int Z = 0)
{
    public CoordinatesDto(Coordinates coordinates) : this(
        coordinates.X,
        coordinates.Y,
        coordinates.Z
    )
    { }
}