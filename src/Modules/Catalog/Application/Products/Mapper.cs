using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetById;
using CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetById;
using CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetById;
using CustomCADs.Catalog.Application.Products.Queries.Internal.Shared.GetAll;
using CustomCADs.Catalog.Domain.Products.ValueObjects;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Extensions;

namespace CustomCADs.Catalog.Application.Products;

internal static class Mapper
{
    internal static GetAllProductsDto ToGetAllDto(this Product product, string username, string categoryName, string timeZone)
        => new(
            Id: product.Id,
            Name: product.Name,
            Status: product.Status.ToString(),
            Views: product.Counts.Views,
            UploadedAt: product.UploadedAt.ToUserLocalTime(timeZone),
            Category: new(product.CategoryId, categoryName),
            CreatorName: username
        );

    internal static GalleryGetProductByIdDto ToGalleryGetByIdDto(this Product product, decimal volume, string username, string categoryName, string timeZone, CoordinatesDto camCoords, CoordinatesDto panCoords)
        => new(
            Id: product.Id,
            Name: product.Name,
            Description: product.Description,
            Price: product.Price,
            Volume: volume,
            CreatorName: username,
            UploadedAt: product.UploadedAt.ToUserLocalTime(timeZone),
            CamCoordinates: camCoords,
            PanCoordinates: panCoords,
            Counts: product.Counts.ToDto(),
            Category: new(product.CategoryId, categoryName)
        );

    internal static CreatorGetProductByIdDto ToCreatorGetByIdDto(this Product product, string username, string categoryName, string timeZone)
        => new(
            Id: product.Id,
            Name: product.Name,
            Description: product.Description,
            Price: product.Price,
            UploadedAt: product.UploadedAt.ToUserLocalTime(timeZone),
            Status: product.Status.ToString(),
            Counts: product.Counts.ToDto(),
            Category: new(product.CategoryId, categoryName),
            CreatorName: username
        );

    internal static DesignerGetProductByIdDto ToDesignerGetByIdDto(this Product product, string username, string categoryName)
        => new(
            Id: product.Id,
            Name: product.Name,
            Description: product.Description,
            Price: product.Price,
            Category: new(product.CategoryId, categoryName),
            CreatorName: username
        );

    internal static CountsDto ToDto(this Counts counts)
        => new(
            Purchases: counts.Purchases,
            Views: counts.Views
        );
}
