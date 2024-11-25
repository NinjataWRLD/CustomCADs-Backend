using CustomCADs.Inventory.Application.Products.Queries.GetAll;
using CustomCADs.Inventory.Endpoints.Designer.Get;

namespace CustomCADs.Inventory.Endpoints.Designer;

using static Constants;

public static class Mapper
{
    public static GetUncheckedProductsDto ToGetUncheckedProductsDto(this GetAllProductsDto item)
        => new(
            Id: item.Id.Value,
            Name: item.Name,
            UploadDate: item.UploadDate.ToString(DateFormatString),
            Image: new(item.Image.Key, item.Image.ContentType),
            CreatorName: item.CreatorName,
            Category: new(item.Category.Id.Value, item.Category.Name)
        );
}
