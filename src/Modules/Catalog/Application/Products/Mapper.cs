using CustomCADs.Catalog.Application.Products.Queries.Creator.GetById;
using CustomCADs.Catalog.Application.Products.Queries.Designer.GetById;
using CustomCADs.Catalog.Application.Products.Queries.Gallery.GetById;
using CustomCADs.Catalog.Application.Products.Queries.Shared.GetAll;
using CustomCADs.Catalog.Domain.Products.ValueObjects;
using CustomCADs.Catalog.Endpoints.Common.Dtos;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Catalog.Application.Products;

internal static class Mapper
{
    internal static GetAllProductsDto ToGetAllDto(this Product product, string username, string categoryName, string timeZone)
        => new(
            Id: product.Id,
            Name: product.Name,
            Status: product.Status.ToString(),
            Views: product.Counts.Views,
            UploadDate: TimeZoneInfo.ConvertTimeFromUtc(
                product.UploadDate,
                TimeZoneInfo.FindSystemTimeZoneById(timeZone)
            ),
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
            UploadDate: TimeZoneInfo.ConvertTimeFromUtc(
                product.UploadDate,
                TimeZoneInfo.FindSystemTimeZoneById(timeZone)
            ),
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
            UploadDate: TimeZoneInfo.ConvertTimeFromUtc(
                product.UploadDate,
                TimeZoneInfo.FindSystemTimeZoneById(timeZone)
            ),
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
