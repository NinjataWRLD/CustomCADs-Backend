using CustomCADs.Shared.Core.Common.ValueObjects;

namespace CustomCADs.Inventory.Application.Products.Queries;

public record CadDto(
    string Key,
    string ContentType,
    Coordinates CamCoordinates,
    Coordinates PanCoordinates
);
