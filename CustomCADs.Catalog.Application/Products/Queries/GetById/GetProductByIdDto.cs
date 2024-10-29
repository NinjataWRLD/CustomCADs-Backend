using CustomCADs.Catalog.Application.Categories.Queries;
using CustomCADs.Catalog.Domain.Products.ValueObjects;

namespace CustomCADs.Catalog.Application.Products.Queries.GetById;

public record GetProductByIdDto(
    Guid Id,
    string Name,
    string Description,
    decimal Cost,
    string Status,
    string ImagePath,
    Cad Cad,
    DateTime UploadDate,
    CategoryReadDto Category
);
