using CustomCADs.Catalog.Application.Products.Queries.GetAll;
using CustomCADs.Catalog.Endpoints.Categories;
using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.GetProducts;

using static Constants;

public record GetProductsDto(
    Guid Id,
    string Name,
    string CreatorName,
    string UploadDate,
    ImageDto Image,
    CategoryResponse Category
)
{
    public GetProductsDto(GetAllProductsItem item) : this(
        Id: item.Id,
        Name: item.Name,
        UploadDate: item.UploadDate.ToString(DateFormatString),
        Image: new(item.Image),
        CreatorName: item.CreatorName,
        Category: new(item.Category.Id, item.Category.Name)
    )
    { }
}
