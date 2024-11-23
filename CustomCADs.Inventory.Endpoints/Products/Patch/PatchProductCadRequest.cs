using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Inventory.Endpoints.Products.Patch;

public record PatchProductCadRequest(
    Guid Id,
    string Type,
    CoordinatesDto Coordinates
);
