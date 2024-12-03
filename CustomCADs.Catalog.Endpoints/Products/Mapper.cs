using CustomCADs.Catalog.Application.Common.Dtos;
using CustomCADs.Catalog.Application.Products.Queries.GetAll;
using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Domain.Products.ValueObjects;
using CustomCADs.Catalog.Endpoints.Common.Dtos;
using CustomCADs.Catalog.Endpoints.Products.Get.All;
using CustomCADs.Catalog.Endpoints.Products.Get.Recent;
using CustomCADs.Catalog.Endpoints.Products.Get.Single;
using CustomCADs.Catalog.Endpoints.Products.Post.Products;

namespace CustomCADs.Catalog.Endpoints.Products;

using static Constants;

internal static class Mapper
{
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

    internal static CountsDto ToCountsDto(this Counts counts)
        => new(
            Purchases: counts.Purchases,
            Likes: counts.Likes,
            Views: counts.Views
        );

    internal static CategoryResponse ToCategoryDto(this CategoryDto category)
        => new(
            Id: category.Id.Value,
            Name: category.Name
        );

    internal static ImageDto ToImageDto(this Image image)
        => new(
            Key: image.Key,
            ContentType: image.ContentType
        );
}
