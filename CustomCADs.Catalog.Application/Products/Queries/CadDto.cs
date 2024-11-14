using CustomCADs.Shared.Core.Domain.ValueObjects;

namespace CustomCADs.Catalog.Application.Products.Queries;

public record CadDto(
    string Path,
    Coordinates CamCoordinates,
    Coordinates PanCoordinates
);
