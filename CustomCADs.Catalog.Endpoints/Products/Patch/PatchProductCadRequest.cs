using CustomCADs.Catalog.Endpoints.Helpers.Dtos;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.Patch;

public sealed record PatchProductCadRequest(
    Guid Id,
    CoordinateType Type,
    CoordinatesDto Coordinates
);
