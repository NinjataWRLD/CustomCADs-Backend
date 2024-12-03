using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Catalog.Application.Products.Queries;

public record CadDto(
    string Key,
    string ContentType,
    CoordinatesDto CamCoordinates,
    CoordinatesDto PanCoordinates
);
