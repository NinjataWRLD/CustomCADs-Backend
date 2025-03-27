namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetById;

public record CreatorGetProductByIdDto(
    ProductId Id,
    string Name,
    string Description,
    decimal Price,
    string Status,
    DateTimeOffset UploadedAt,
    CountsDto Counts,
    string CreatorName,
    CategoryDto Category
);

