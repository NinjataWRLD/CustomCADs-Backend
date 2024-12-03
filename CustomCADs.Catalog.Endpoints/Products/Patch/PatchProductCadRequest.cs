using CustomCADs.Catalog.Endpoints.Common.Dtos;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.Patch;

public sealed record PatchProductCadRequest(
    Guid Id,
    CoordinateType Type,
    CoordinatesDto Coordinates
);
