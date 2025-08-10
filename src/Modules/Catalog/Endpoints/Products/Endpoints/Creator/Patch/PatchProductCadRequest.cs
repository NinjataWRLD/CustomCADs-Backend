using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Patch;

public sealed record PatchProductCadRequest(
	Guid Id,
	CoordinateType Type,
	CoordinatesDto Coordinates
);
