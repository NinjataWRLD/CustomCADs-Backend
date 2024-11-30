using CustomCADs.Inventory.Application.Products.Queries.DesignerGetById;
using CustomCADs.Inventory.Application.Products.Queries.GetAll;
using CustomCADs.Inventory.Endpoints.Designer.Get.All;
using CustomCADs.Inventory.Endpoints.Designer.Get.Single;

namespace CustomCADs.Inventory.Endpoints.Designer;

using static Constants;

public static class Mapper
{
    public static GetUncheckedProductsDto ToGetUncheckedProductsDto(this GetAllProductsDto product)
        => new(
            Id: product.Id.Value,
            Name: product.Name,
            UploadDate: product.UploadDate.ToString(DateFormatString),
            Image: new(product.Image.Key, product.Image.ContentType),
            CreatorName: product.CreatorName,
            Category: new(product.Category.Id.Value, product.Category.Name)
        );

    public static DesignerSingleProductResponse ToDesignerSingleProductResponse(this DesignerGetProductByIdDto product)
        => new(
            Id: product.Id.Value,
            Name: product.Name,
            Description: product.Description,
            Price: product.Price.Amount,
            CreatorName: product.CreatorName,
            Cad: product.Cad,
            Category: new(product.Category.Id.Value, product.Category.Name)
        );
}
