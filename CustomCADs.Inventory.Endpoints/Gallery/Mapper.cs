using CustomCADs.Inventory.Application.Products;
using CustomCADs.Inventory.Application.Products.Queries.GalleryGetById;
using CustomCADs.Inventory.Application.Products.Queries.GetAll;
using CustomCADs.Inventory.Endpoints.Gallery.Get.All;
using CustomCADs.Inventory.Endpoints.Gallery.Get.Single;
using CustomCADs.Inventory.Endpoints.Products;

namespace CustomCADs.Inventory.Endpoints.Gallery;

using static Constants;

internal static class Mapper
{
    internal static GetAllGaleryProductsResponse ToGetAllGaleryProductsResponse(this GetAllProductsDto product)
        => new(
            Id: product.Id.Value,
            Name: product.Name,
            Image: product.Image.ToImageDto()
        );

    internal static GetGalleryProductResponse ToGetGalleryProductResponse(this GalleryGetProductByIdDto product)
        => new(
            Id: product.Id.Value,
            Name: product.Name,
            Description: product.Description,
            Price: product.Price,
            UploadDate: product.UploadDate.ToString(DateFormatString),
            CadKey: product.Cad.Key,
            CamCoordinates: product.Cad.CamCoordinates,
            PanCoordinates: product.Cad.PanCoordinates,
            Counts: product.Counts.ToCountsDto(),
            Category: product.Category.ToCategoryDto()
        );
}
