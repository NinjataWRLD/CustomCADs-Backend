using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Inventory.Application.Products.Queries;

public record CadDto(
    string Key,
    string ContentType,
    CoordinatesDto CamCoordinates,
    CoordinatesDto PanCoordinates
);
