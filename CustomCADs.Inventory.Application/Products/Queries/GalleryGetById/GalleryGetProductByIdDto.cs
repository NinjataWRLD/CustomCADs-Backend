using CustomCADs.Inventory.Domain.Products.ValueObjects;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Inventory.Application.Products.Queries.GalleryGetById;

public record GalleryGetProductByIdDto(
    ProductId Id,
    string Name,
    string Description,
    decimal Price,
    string CreatorName,
    DateTime UploadDate,
    Counts Counts,
    CadDto Cad,
    (CategoryId Id, string Name) Category
);