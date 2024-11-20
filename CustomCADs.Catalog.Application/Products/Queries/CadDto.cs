using CustomCADs.Shared.Core.Domain.ValueObjects;

namespace CustomCADs.Catalog.Application.Products.Queries;

public record CadDto(
    string Key,
    string ContentType,
    Coordinates CamCoordinates,
    Coordinates PanCoordinates
);
