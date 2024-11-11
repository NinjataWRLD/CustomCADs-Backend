using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.PatchProduct;

public record PatchCadRequest(ProductId Id, string Type, CoordinatesDto Coordinates);
