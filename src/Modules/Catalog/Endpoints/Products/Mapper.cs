using CustomCADs.Catalog.Application.Products.Enums;
using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetAll;
using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetById;
using CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetAll;
using CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetById;
using CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetAll;
using CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetById;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Get.All;
using CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Get.Recent;
using CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Get.Single;
using CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Post.Products;
using CustomCADs.Catalog.Endpoints.Products.Endpoints.Designer.Get.Reported;
using CustomCADs.Catalog.Endpoints.Products.Endpoints.Designer.Get.Single;
using CustomCADs.Catalog.Endpoints.Products.Endpoints.Designer.Get.Unchecked;
using CustomCADs.Catalog.Endpoints.Products.Endpoints.Designer.Get.Validated;
using CustomCADs.Catalog.Endpoints.Products.Endpoints.Gallery.Get.All;
using CustomCADs.Catalog.Endpoints.Products.Endpoints.Gallery.Get.Single;

namespace CustomCADs.Catalog.Endpoints.Products;

internal static class Mapper
{
    internal static GetAllGaleryProductsResponse ToResponse(this GalleryGetAllProductsDto product)
        => new(
            Id: product.Id.Value,
            Name: product.Name,
            Tags: product.Tags,
            Category: product.Category.Name,
            Views: product.Views
        );

    internal static GetGalleryProductResponse ToResponse(this GalleryGetProductByIdDto product)
        => new(
            Id: product.Id.Value,
            Name: product.Name,
            Description: product.Description,
            Price: product.Price,
            Volume: product.Volume,
            Tags: product.Tags,
            CreatorName: product.CreatorName,
            UploadedAt: product.UploadedAt,
            CamCoordinates: product.CamCoordinates,
            PanCoordinates: product.PanCoordinates,
            Counts: product.Counts,
            Category: product.Category.ToResponse()
        );

    internal static GetProductsResponse ToResponse(this CreatorGetAllProductsDto product)
        => new(
            Id: product.Id.Value,
            Name: product.Name,
            UploadedAt: product.UploadedAt,
            Category: product.Category.ToResponse()
        );

    internal static RecentProductsResponse ToRecentResponse(this CreatorGetAllProductsDto product)
        => new(
            Id: product.Id.Value,
            Name: product.Name,
            Status: product.Status,
            UploadedAt: product.UploadedAt,
            Category: product.Category.ToResponse()
        );

    internal static GetProductResponse ToGetResponse(this CreatorGetProductByIdDto product)
        => new(
            Id: product.Id.Value,
            Name: product.Name,
            Price: product.Price,
            Description: product.Description,
            UploadedAt: product.UploadedAt,
            Counts: product.Counts,
            Category: product.Category.ToResponse()
        );

    internal static PostProductResponse ToPostResponse(this CreatorGetProductByIdDto product)
        => new(
            Id: product.Id.Value,
            Name: product.Name,
            Description: product.Description,
            Price: product.Price,
            Status: product.Status,
            UploadedAt: product.UploadedAt,
            CreatorName: product.CreatorName,
            Category: product.Category.ToResponse()
        );

    internal static GetUncheckedProductsResponse ToGetUncheckedDto(this DesignerGetAllProductsDto product)
    => new(
        Id: product.Id.Value,
        Name: product.Name,
        UploadedAt: product.UploadedAt,
        CreatorName: product.CreatorName,
        Category: new(product.Category.Id.Value, product.Category.Name)
    );

    internal static GetValidatedProductsResponse ToGetValidatedDto(this DesignerGetAllProductsDto product)
    => new(
        Id: product.Id.Value,
        Name: product.Name,
        UploadedAt: product.UploadedAt,
        CreatorName: product.CreatorName,
        Category: new(product.Category.Id.Value, product.Category.Name)
    );

    internal static GetReportedProductsResponse ToGetReportedDto(this DesignerGetAllProductsDto product)
    => new(
        Id: product.Id.Value,
        Name: product.Name,
        UploadedAt: product.UploadedAt,
        CreatorName: product.CreatorName,
        Category: new(product.Category.Id.Value, product.Category.Name)
    );

    internal static DesignerSingleProductResponse ToResponse(this DesignerGetProductByIdDto product)
        => new(
            Id: product.Id.Value,
            Name: product.Name,
            Description: product.Description,
            Price: product.Price,
            CreatorName: product.CreatorName,
            Category: new(product.Category.Id.Value, product.Category.Name)
        );

    internal static CategoryResponse ToResponse(this CategoryDto category)
        => new(
            Id: category.Id.Value,
            Name: category.Name
        );

    internal static ProductSortingType ToBase(this ProductGallerySortingType sorting)
        => sorting switch
        {
            ProductGallerySortingType.UploadedAt => ProductSortingType.UploadedAt,
            ProductGallerySortingType.Alphabetical => ProductSortingType.Alphabetical,
            ProductGallerySortingType.Cost => ProductSortingType.Cost,
            ProductGallerySortingType.Purchases => ProductSortingType.Purchases,
            ProductGallerySortingType.Views => ProductSortingType.Views,
            _ => throw new ArgumentException("", nameof(sorting))
        };

    internal static ProductSortingType ToBase(this ProductCreatorSortingType sorting)
        => sorting switch
        {
            ProductCreatorSortingType.UploadedAt => ProductSortingType.UploadedAt,
            ProductCreatorSortingType.Alphabetical => ProductSortingType.Alphabetical,
            ProductCreatorSortingType.Status => ProductSortingType.Status,
            ProductCreatorSortingType.Cost => ProductSortingType.Cost,
            ProductCreatorSortingType.Purchases => ProductSortingType.Purchases,
            ProductCreatorSortingType.Views => ProductSortingType.Views,
            _ => throw new ArgumentException("", nameof(sorting))
        };

    internal static ProductSortingType ToBase(this ProductDesignerSortingType sorting)
        => sorting switch
        {
            ProductDesignerSortingType.UploadedAt => ProductSortingType.UploadedAt,
            ProductDesignerSortingType.Alphabetical => ProductSortingType.Alphabetical,
            ProductDesignerSortingType.Cost => ProductSortingType.Cost,
            _ => throw new ArgumentException("", nameof(sorting))
        };
}
