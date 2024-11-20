using CustomCADs.Shared.Core.Domain.ValueObjects;

namespace CustomCADs.Catalog.Application.Products.Queries;

public record CadDto(
    string Key,
    Coordinates CamCoordinates,
    Coordinates PanCoordinates
);
