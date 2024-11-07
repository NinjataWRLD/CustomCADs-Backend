using CustomCADs.Catalog.Application.Products.Queries.GetAll;
using CustomCADs.Catalog.Endpoints.Categories;

namespace CustomCADs.Catalog.Endpoints.Products.GetProducts;

using static Constants;

public record GetProductsDto(
    Guid Id,
    string Name,
    string CreatorName,
    string UploadDate,
    string ImagePath,
    CategoryResponse Category
)
{
    public GetProductsDto(GetAllProductsItem item) : this(
        Id: item.Id,
        Name: item.Name,
        UploadDate: item.UploadDate.ToString(DateFormatString),
        ImagePath: item.ImagePath,
        CreatorName: item.CreatorName,
        Category: new(item.Category.Id, item.Category.Name)
    )
    { }
}
