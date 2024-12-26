using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.Contributors.Patch;

public sealed record PatchProductCadRequest(
    Guid Id,
    CoordinateType Type,
    CoordinatesDto Coordinates
);
