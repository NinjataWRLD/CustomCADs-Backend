using CustomCADs.Catalog.Domain.Products.ValueObjects;

namespace CustomCADs.Catalog.Application.Products.Commands.SetCad;

public record SetProductCadDto(
    string Path,
    Coordinates CamCoordinates,
    Coordinates PanCoordinates
);
