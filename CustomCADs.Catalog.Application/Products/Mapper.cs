using CustomCADs.Catalog.Application.Products.Queries.DesignerGetById;
using CustomCADs.Catalog.Application.Products.Queries.GalleryGetById;
using CustomCADs.Catalog.Application.Products.Queries.GetAll;
using CustomCADs.Catalog.Application.Products.Queries.GetById;

namespace CustomCADs.Catalog.Application.Products;

internal static class Mapper
{
    internal static GalleryGetProductByIdDto ToGalleryGetProductByIdDto(this Product product, string username, string categoryName, string timeZone)
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
            CadId: product.CadId,
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
            ImageId: product.ImageId,
            Category: new(product.CategoryId, categoryName),
            CreatorName: username
        );

    internal static GetProductByIdDto ToGetProductByIdDto(this Product product, string username, string categoryName, string timeZone)
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
            Counts: product.Counts,
            CadId: product.CadId,
            Category: new(product.CategoryId, categoryName),
            CreatorName: username
        );

    internal static DesignerGetProductByIdDto ToDesignerGetProductByIdDto(this Product product, string username, string categoryName)
        => new(
            Id: product.Id,
            Name: product.Name,
            Description: product.Description,
            Price: product.Price,
            CadId: product.CadId,
            Category: new(product.CategoryId, categoryName),
            CreatorName: username
        );
}
