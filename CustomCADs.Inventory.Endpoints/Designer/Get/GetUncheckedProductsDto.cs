using CustomCADs.Inventory.Endpoints.Products;
using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Inventory.Endpoints.Designer.Get;

public record GetUncheckedProductsDto(
    Guid Id,
    string Name,
    string UploadDate,
    string CreatorName,
    ImageDto Image,
    CategoryDto Category
);