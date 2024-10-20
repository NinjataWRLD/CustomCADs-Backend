using CustomCADs.Catalog.Domain.Products.ValueObjects;

namespace CustomCADs.Catalog.Application.Products.Commands.EditCad;

public class SetProductCadDto
{
    public required string Path { get; set; }
    public required Coordinates CamCoordinates { get; set; }
    public required Coordinates PanCoordinates { get; set; }
}
