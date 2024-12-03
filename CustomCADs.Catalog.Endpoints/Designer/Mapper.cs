using CustomCADs.Catalog.Application.Products.Queries.DesignerGetById;
using CustomCADs.Catalog.Application.Products.Queries.GetAll;
using CustomCADs.Catalog.Endpoints.Designer.Get.All;
using CustomCADs.Catalog.Endpoints.Designer.Get.Single;

namespace CustomCADs.Catalog.Endpoints.Designer;

using static Constants;

internal static class Mapper
{
    internal static GetUncheckedProductsDto ToGetUncheckedProductsDto(this GetAllProductsDto product)
        => new(
            Id: product.Id.Value,
            Name: product.Name,
            UploadDate: product.UploadDate.ToString(DateFormatString),
            Image: new(product.Image.Key, product.Image.ContentType),
            CreatorName: product.CreatorName,
            Category: new(product.Category.Id.Value, product.Category.Name)
        );

    internal static DesignerSingleProductResponse ToDesignerSingleProductResponse(this DesignerGetProductByIdDto product)
        => new(
            Id: product.Id.Value,
            Name: product.Name,
            Description: product.Description,
            Price: product.Price,
            CreatorName: product.CreatorName,
            Cad: product.Cad,
            Category: new(product.Category.Id.Value, product.Category.Name)
        );
}
