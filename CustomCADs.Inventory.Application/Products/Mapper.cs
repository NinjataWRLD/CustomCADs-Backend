using CustomCADs.Inventory.Application.Products.Queries;
using CustomCADs.Inventory.Application.Products.Queries.DesignerGetById;
using CustomCADs.Inventory.Application.Products.Queries.GalleryGetById;
using CustomCADs.Inventory.Application.Products.Queries.GetAll;
using CustomCADs.Inventory.Application.Products.Queries.GetById;
using CustomCADs.Inventory.Domain.Products;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Inventory.Application.Products;

internal static class Mapper
{
    internal static GalleryGetProductByIdDto ToGalleryGetProductByIdDto(this Product product, CadDto cad, string username, string categoryName, string timeZone)
        => new(
            Id: product.Id,
            Name: product.Name,
            Description: product.Description,
            Price: product.Price,
            CreatorName: username,
            UploadDate: TimeZoneInfo.ConvertTimeFromUtc(
                product.UploadDate,
                TimeZoneInfo.FindSystemTimeZoneById(timeZone)
            ),
            Counts: product.Counts,
            Cad: cad,
            Category: new(product.CategoryId, categoryName)
        );

    internal static GetAllProductsDto ToGetAllProductsItem(this Product product, string username, string categoryName, string timeZone)
        => new(
            Id: product.Id,
            Name: product.Name,
            Status: product.Status.ToString(),
            UploadDate: TimeZoneInfo.ConvertTimeFromUtc(
                product.UploadDate,
                TimeZoneInfo.FindSystemTimeZoneById(timeZone)
            ),
            Image: product.Image,
            Category: new(product.CategoryId, categoryName),
            CreatorName: username
        );

    internal static GetProductByIdDto ToGetProductByIdDto(this Product product, CadDto cad, string username, string categoryName, string timeZone)
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
            Image: product.Image,
            Counts: product.Counts,
            Cad: cad,
            Category: new(product.CategoryId, categoryName),
            CreatorName: username
        );

    internal static DesignerGetProductByIdDto ToDesignerGetProductByIdDto(this Product product, CadDto cad, string username, string categoryName)
        => new(
            Id: product.Id,
            Name: product.Name,
            Description: product.Description,
            Price: product.Price,
            Cad: cad,
            Category: new(product.CategoryId, categoryName),
            CreatorName: username
        );

    internal static CadDto ToCadDto(this
        (string Key,
        string ContentType,
        CoordinatesDto CamCoordinates,
        CoordinatesDto PanCoordinates) cad
    ) => new(cad.Key, cad.ContentType, cad.CamCoordinates, cad.PanCoordinates);
}
