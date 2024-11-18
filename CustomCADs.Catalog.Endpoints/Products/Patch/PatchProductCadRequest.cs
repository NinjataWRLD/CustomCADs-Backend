using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.Patch;

public record PatchProductCadRequest(
    Guid Id,
    string Type,
    CoordinatesDto Coordinates
);
