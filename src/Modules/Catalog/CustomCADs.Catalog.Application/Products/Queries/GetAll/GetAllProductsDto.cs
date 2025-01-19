using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Catalog.Application.Products.Queries.GetAll;

public record GetAllProductsDto(
    ProductId Id,
    string Name,
    string Status,
    DateTime UploadDate,
    ImageId ImageId,
    string CreatorName,
    CategoryDto Category
);
