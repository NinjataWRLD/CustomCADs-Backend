using CustomCADs.Catalog.Application.Common.Enums;
using CustomCADs.Catalog.Application.Products.Queries.Creator.GetById;
using CustomCADs.Catalog.Application.Products.Queries.Designer.GetById;
using CustomCADs.Catalog.Application.Products.Queries.Gallery.GetById;
using CustomCADs.Catalog.Application.Products.Queries.Shared.GetAll;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Endpoints.Products.Creator.Get.All;
using CustomCADs.Catalog.Endpoints.Products.Creator.Get.Recent;
using CustomCADs.Catalog.Endpoints.Products.Creator.Get.Single;
using CustomCADs.Catalog.Endpoints.Products.Creator.Post.Products;
using CustomCADs.Catalog.Endpoints.Products.Designer.Get.Reported;
using CustomCADs.Catalog.Endpoints.Products.Designer.Get.Single;
using CustomCADs.Catalog.Endpoints.Products.Designer.Get.Unchecked;
using CustomCADs.Catalog.Endpoints.Products.Designer.Get.Validated;
using CustomCADs.Catalog.Endpoints.Products.Gallery.Get.All;
using CustomCADs.Catalog.Endpoints.Products.Gallery.Get.Single;

namespace CustomCADs.Catalog.Endpoints.Products;

using static Constants;

internal static class Mapper
{
    internal static GetAllGaleryProductsResponse ToGalleryGetAllResponse(this GetAllProductsDto product)
        => new(
            Id: product.Id.Value,
            Name: product.Name,
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
            CreatorName: product.CreatorName,
            UploadDate: product.UploadDate.ToString(DateFormatString),
            CamCoordinates: product.CamCoordinates,
            PanCoordinates: product.PanCoordinates,
            Counts: product.Counts,
            Category: product.Category.ToResponse()
        );

    internal static RecentProductsResponse ToRecentResponse(this GetAllProductsDto product)
    => new(
        Id: product.Id.Value,
        Name: product.Name,
        Status: product.Status,
        UploadDate: product.UploadDate.ToString(DateFormatString),
        Category: product.Category.ToResponse()
    );

    internal static GetProductsResponse ToCreatorGetAllResponse(this GetAllProductsDto product)
        => new(
            Id: product.Id.Value,
            Name: product.Name,
            UploadDate: product.UploadDate.ToString(DateFormatString),
            CreatorName: product.CreatorName,
            Category: product.Category.ToResponse()
        );

    internal static GetProductResponse ToGetResponse(this CreatorGetProductByIdDto product)
        => new(
            Id: product.Id.Value,
            Name: product.Name,
            Price: product.Price,
            Description: product.Description,
            UploadDate: product.UploadDate.ToString(DateFormatString),
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
            UploadDate: product.UploadDate.ToString(DateFormatString),
            CreatorName: product.CreatorName,
            Category: product.Category.ToResponse()
        );

    internal static GetUncheckedProductsResponse ToGetUncheckedDto(this GetAllProductsDto product)
    => new(
        Id: product.Id.Value,
        Name: product.Name,
        UploadDate: product.UploadDate.ToString(DateFormatString),
        CreatorName: product.CreatorName,
        Category: new(product.Category.Id.Value, product.Category.Name)
    );

    internal static GetValidatedProductsResponse ToGetValidatedDto(this GetAllProductsDto product)
    => new(
        Id: product.Id.Value,
        Name: product.Name,
        UploadDate: product.UploadDate.ToString(DateFormatString),
        CreatorName: product.CreatorName,
        Category: new(product.Category.Id.Value, product.Category.Name)
    );

    internal static GetReportedProductsResponse ToGetReportedDto(this GetAllProductsDto product)
    => new(
        Id: product.Id.Value,
        Name: product.Name,
        UploadDate: product.UploadDate.ToString(DateFormatString),
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
            ProductGallerySortingType.UploadDate => ProductSortingType.UploadDate,
            ProductGallerySortingType.Alphabetical => ProductSortingType.Alphabetical,
            ProductGallerySortingType.Cost => ProductSortingType.Cost,
            ProductGallerySortingType.Purchases => ProductSortingType.Purchases,
            ProductGallerySortingType.Views => ProductSortingType.Views,
            _ => throw new ArgumentException("", nameof(sorting))
        };

    internal static ProductSortingType ToBase(this ProductCreatorSortingType sorting)
        => sorting switch
        {
            ProductCreatorSortingType.UploadDate => ProductSortingType.UploadDate,
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
            ProductDesignerSortingType.UploadDate => ProductSortingType.UploadDate,
            ProductDesignerSortingType.Alphabetical => ProductSortingType.Alphabetical,
            ProductDesignerSortingType.Cost => ProductSortingType.Cost,
            _ => throw new ArgumentException("", nameof(sorting))
        };
}
