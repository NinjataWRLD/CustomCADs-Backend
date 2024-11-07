using CustomCADs.Catalog.Application.Categories.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.GetAll;

public record GetAllProductsDto(int Count, ICollection<GetAllProductsItem> Products);

public record GetAllProductsItem(
    Guid Id,
    string Name,
    string Status,
    DateTime UploadDate,
    string ImagePath,
    string CreatorName,
    CategoryReadDto Category
)
{
    public GetAllProductsItem(Product product, string username) : this(
        Id: product.Id,
        Name: product.Name,
        Status: product.Status.ToString(),
        UploadDate: product.UploadDate,
        ImagePath: product.ImagePath,
        Category: new(product.CategoryId, product.Category.Name),
        CreatorName: username
    )
    { }
}
