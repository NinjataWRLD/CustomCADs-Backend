using CustomCADs.Catalog.Application.Categories.Queries;
using CustomCADs.Shared.Core.Domain.ValueObjects;

namespace CustomCADs.Catalog.Application.Products.Queries.GetById;

public record GetProductByIdDto(
    Guid Id,
    string Name,
    string Description,
    decimal Cost,
    string Status,
    string ImagePath,
    string CreatorName,
    Cad Cad,
    DateTime UploadDate,
    CategoryReadDto Category
)
{
    public GetProductByIdDto(Product product, string username) : this(
        Id: product.Id,
        Name: product.Name,
        Description: product.Description,
        Cost: product.Cost,
        UploadDate: product.UploadDate,
        Status: product.Status.ToString(),
        ImagePath: product.ImagePath,
        Cad: product.Cad,
        Category: new(product.Category.Id, product.Category.Name),
        CreatorName: username
    )
    { }
}

