using CustomCADs.Inventory.Application.Common.Dtos;
using CustomCADs.Inventory.Domain.Products.ValueObjects;

namespace CustomCADs.Inventory.Application.Products.Queries.GetAll;

public record GetAllProductsDto(
    ProductId Id,
    string Name,
    string Status,
    DateTime UploadDate,
    Image Image,
    string CreatorName,
    CategoryDto Category
);
