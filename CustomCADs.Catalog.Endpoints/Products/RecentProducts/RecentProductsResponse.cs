using CustomCADs.Catalog.Application.Products.Queries.GetAll;
using CustomCADs.Catalog.Endpoints.Categories;

namespace CustomCADs.Catalog.Endpoints.Products.RecentProducts;

using static Constants;

public record RecentProductsResponse(
    Guid Id,
    string Name,
    string Status,
    string UploadDate,
    CategoryResponse Category
)
{
    public RecentProductsResponse(GetAllProductsItem item) : this(
        Id: item.Id,
        Name: item.Name,
        Status: item.Status,
        UploadDate: item.UploadDate.ToString(DateFormatString),
        Category: new(item.Category.Id, item.Category.Name)
    )
    { }
}

