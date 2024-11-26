using CustomCADs.Inventory.Domain.Products.ValueObjects;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Inventory.Application.Products.Queries.GetAll;

public record GetAllProductsDto(
    ProductId Id,
    string Name,
    string Status,
    DateTime UploadDate,
    Image Image,
    string CreatorName,
    (CategoryId Id, string Name) Category
);
