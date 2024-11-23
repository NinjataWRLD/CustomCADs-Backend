using CustomCADs.Shared.Core.Domain.ValueObjects;

namespace CustomCADs.Inventory.Application.Products.Queries;

public record CadDto(
    string Key,
    string ContentType,
    Coordinates CamCoordinates,
    Coordinates PanCoordinates
);
