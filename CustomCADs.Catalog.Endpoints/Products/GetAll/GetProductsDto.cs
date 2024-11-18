using CustomCADs.Catalog.Endpoints.Categories;
using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.GetAll;

public record GetProductsDto(
    Guid Id,
    string Name,
    string CreatorName,
    string UploadDate,
    ImageDto Image,
    CategoryResponse Category
);
