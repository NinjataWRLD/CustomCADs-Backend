using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;
using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.PutProduct;

public record PutProductRequest(
    ProductId Id,
    string Name,
    string Description,
    int CategoryId,
    MoneyDto Price,
    IFormFile? Image = default
);
