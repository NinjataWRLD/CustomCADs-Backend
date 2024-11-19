using CustomCADs.Catalog.Application.Products.Queries.GetAll;
using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Endpoints.Products.Get.All;
using CustomCADs.Catalog.Endpoints.Products.Get.Recent;
using CustomCADs.Catalog.Endpoints.Products.Get.Single;
using CustomCADs.Catalog.Endpoints.Products.Post;

namespace CustomCADs.Catalog.Endpoints.Products;

using static Constants;

public static class Mapper
{
    public static RecentProductsResponse ToRecentProductsResponse(this GetAllProductsItem item)
        => new(
            Id: item.Id.Value,
            Name: item.Name,
            Status: item.Status,
            UploadDate: item.UploadDate.ToString(DateFormatString),
            Category: new(item.Category.Id.Value, item.Category.Name)
        );

    public static GetProductsDto ToGetProductsDto(this GetAllProductsItem item)
        => new(
            Id: item.Id.Value,
            Name: item.Name,
            UploadDate: item.UploadDate.ToString(DateFormatString),
            Image: new(item.Image),
            CreatorName: item.CreatorName,
            Category: new(item.Category.Id.Value, item.Category.Name)
        );

    public static GetProductResponse ToGetProductResponse(this GetProductByIdDto dto)
        => new(
            Id: dto.Id.Value,
            Name: dto.Name,
            Price: new(dto.Price),
            Description: dto.Description,
            UploadDate: dto.UploadDate.ToString(DateFormatString),
            Category: new() { Id = dto.Category.Id.Value, Name = dto.Category.Name },
            CamCoordinates: dto.Cad is null ? new() : new(dto.Cad.CamCoordinates),
            PanCoordinates: dto.Cad is null ? new() : new(dto.Cad.PanCoordinates),
            CadPath: dto.Cad is null ? string.Empty : dto.Cad.Path
        );

    public static PostProductResponse ToPostProductResponse(this GetProductByIdDto dto)
        => new(
            Id: dto.Id,
            Name: dto.Name,
            Description: dto.Description,
            Price: new(dto.Price),
            Status: dto.Status,
            UploadDate: dto.UploadDate.ToString(DateFormatString),
            CreatorName: dto.CreatorName,
            Category: new() { Id = dto.Category.Id.Value, Name = dto.Category.Name }
        );
}
