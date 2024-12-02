using CustomCADs.Inventory.Application.Common.Dtos;
using CustomCADs.Inventory.Application.Products;
using CustomCADs.Inventory.Application.Products.Queries.GetAll;
using CustomCADs.Inventory.Application.Products.Queries.GetById;
using CustomCADs.Inventory.Domain.Products.ValueObjects;
using CustomCADs.Inventory.Endpoints.Helpers.Dtos;
using CustomCADs.Inventory.Endpoints.Products.Get.All;
using CustomCADs.Inventory.Endpoints.Products.Get.Recent;
using CustomCADs.Inventory.Endpoints.Products.Get.Single;
using CustomCADs.Inventory.Endpoints.Products.Post;

namespace CustomCADs.Inventory.Endpoints.Products;

using static Constants;

public static class Mapper
{
    public static RecentProductsResponse ToRecentProductsResponse(this GetAllProductsDto product)
        => new(
            Id: product.Id.Value,
            Name: product.Name,
            Status: product.Status,
            UploadDate: product.UploadDate.ToString(DateFormatString),
            Category: product.Category.ToCategoryDto()
        );

    public static GetProductsResponse ToGetProductsDto(this GetAllProductsDto product)
        => new(
            Id: product.Id.Value,
            Name: product.Name,
            UploadDate: product.UploadDate.ToString(DateFormatString),
            Image: product.Image.ToImageDto(),
            CreatorName: product.CreatorName,
            Category: product.Category.ToCategoryDto()
        );

    public static GetProductResponse ToGetProductResponse(this GetProductByIdDto product)
        => new(
            Id: product.Id.Value,
            Name: product.Name,
            Price: product.Price,
            Description: product.Description,
            UploadDate: product.UploadDate.ToString(DateFormatString),
            CadKey: product.Cad.Key,
            CamCoordinates: product.Cad.CamCoordinates.ToCoordinatesDto(),
            PanCoordinates: product.Cad.PanCoordinates.ToCoordinatesDto(),
            Counts: product.Counts.ToCountsDto(),
            Category: product.Category.ToCategoryDto()
        );

    public static PostProductResponse ToPostProductResponse(this GetProductByIdDto product)
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

    public static CountsDto ToCountsDto(this Counts counts)
        => new(
            Purchases: counts.Purchases,
            Likes: counts.Likes,
            Views: counts.Views
        );

    public static CategoryResponse ToCategoryDto(this CategoryDto category)
        => new(
            Id: category.Id.Value,
            Name: category.Name
        );

    public static ImageDto ToImageDto(this Image image)
        => new(
            Key: image.Key,
            ContentType: image.ContentType
        );
}
