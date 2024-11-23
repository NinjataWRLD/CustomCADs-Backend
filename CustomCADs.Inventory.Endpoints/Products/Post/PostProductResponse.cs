using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Inventory;
using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Inventory.Endpoints.Products.Post;

public record PostProductResponse(
    ProductId Id,
    string Name,
    string Description,
    string CreatorName,
    string UploadDate,
    MoneyDto Price,
    string Status,
    CategoryDto Category
);
