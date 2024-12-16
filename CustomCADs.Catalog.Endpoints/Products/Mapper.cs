using CustomCADs.Catalog.Application.Common.Dtos;
using CustomCADs.Catalog.Application.Products.Queries.DesignerGetById;
using CustomCADs.Catalog.Application.Products.Queries.GalleryGetById;
using CustomCADs.Catalog.Application.Products.Queries.GetAll;
using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Domain.Products.ValueObjects;
using CustomCADs.Catalog.Endpoints.Common.Dtos;
using CustomCADs.Catalog.Endpoints.Products.Contributors.Get.All;
using CustomCADs.Catalog.Endpoints.Products.Contributors.Get.Recent;
using CustomCADs.Catalog.Endpoints.Products.Contributors.Get.Single;
using CustomCADs.Catalog.Endpoints.Products.Contributors.Post.Products;
using CustomCADs.Catalog.Endpoints.Products.Designer.Get.All;
using CustomCADs.Catalog.Endpoints.Products.Designer.Get.Single;
using CustomCADs.Catalog.Endpoints.Products.Gallery.Get.All;
using CustomCADs.Catalog.Endpoints.Products.Gallery.Get.Single;

namespace CustomCADs.Catalog.Endpoints.Products;

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

    internal static RecentProductsResponse ToRecentProductsResponse(this GetAllProductsDto product)
    => new(
        Id: product.Id.Value,
        Name: product.Name,
        Status: product.Status,
        UploadDate: product.UploadDate.ToString(DateFormatString),
        Category: product.Category.ToCategoryDto()
    );

    internal static GetProductsResponse ToGetProductsDto(this GetAllProductsDto product)
        => new(
            Id: product.Id.Value,
            Name: product.Name,
            UploadDate: product.UploadDate.ToString(DateFormatString),
            Image: product.Image.ToImageDto(),
            CreatorName: product.CreatorName,
            Category: product.Category.ToCategoryDto()
        );

    internal static GetProductResponse ToGetProductResponse(this GetProductByIdDto product)
        => new(
            Id: product.Id.Value,
            Name: product.Name,
            Price: product.Price,
            Description: product.Description,
            UploadDate: product.UploadDate.ToString(DateFormatString),
            CadKey: product.Cad.Key,
            CamCoordinates: product.Cad.CamCoordinates,
            PanCoordinates: product.Cad.PanCoordinates,
            Counts: product.Counts.ToCountsDto(),
            Category: product.Category.ToCategoryDto()
        );

    internal static PostProductResponse ToPostProductResponse(this GetProductByIdDto product)
        => new(
            Id: product.Id.Value,
            Name: product.Name,
            Description: product.Description,
            Price: product.Price,
            Status: product.Status,
            UploadDate: product.UploadDate.ToString(DateFormatString),
            CreatorName: product.CreatorName,
            Category: product.Category.ToCategoryDto()
        );

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

    internal static CountsDto ToCountsDto(this Counts counts)
        => new(
            Purchases: counts.Purchases,
            Likes: counts.Likes,
            Views: counts.Views
        );

    internal static ImageDto ToImageDto(this Image image)
        => new(
            Key: image.Key,
            ContentType: image.ContentType
        );

    internal static CategoryResponse ToCategoryDto(this CategoryDto category)
        => new(
            Id: category.Id.Value,
            Name: category.Name
        );
}
