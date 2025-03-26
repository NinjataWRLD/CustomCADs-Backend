using CustomCADs.Catalog.Endpoints.Products.Dtos;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Patch;

public sealed record PatchProductCadRequest(
    Guid Id,
    CoordinateType Type,
    CoordinatesDto Coordinates
);
