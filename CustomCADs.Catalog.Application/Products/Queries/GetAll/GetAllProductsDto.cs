using CustomCADs.Catalog.Application.Common.Dtos;
using CustomCADs.Catalog.Domain.Products.ValueObjects;

namespace CustomCADs.Catalog.Application.Products.Queries.GetAll;

public record GetAllProductsDto(
    ProductId Id,
    string Name,
    string Status,
    DateTime UploadDate,
    Image Image,
    string CreatorName,
    CategoryDto Category
);
