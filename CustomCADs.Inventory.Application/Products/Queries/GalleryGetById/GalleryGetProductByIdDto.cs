using CustomCADs.Inventory.Application.Common.Dtos;
using CustomCADs.Inventory.Domain.Products.ValueObjects;

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
    CategoryDto Category
);