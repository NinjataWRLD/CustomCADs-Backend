using CustomCADs.Catalog.Application.Products.Queries.GetAll;
using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Endpoints.Products.GetProducts;
using CustomCADs.Catalog.Endpoints.Products.PostProduct;
using CustomCADs.Catalog.Endpoints.Products.RecentProducts;

namespace CustomCADs.Catalog.Endpoints.Helpers;

using static Constants;

public static class Mapper
{
    public static RecentProductsResponse ToRecentProductsResponse(this GetAllProductsItem item)
        => new(
            Id: item.Id,
            Name: item.Name,
            Status: item.Status,
            UploadDate: item.UploadDate.ToString(DateFormatString),
            Category: new(item.Category.Id.Value, item.Category.Name)
        );

    public static GetProductsDto ToGetProductsDto(this GetAllProductsItem item)
        => new(
            Id: item.Id,
            Name: item.Name,
            UploadDate: item.UploadDate.ToString(DateFormatString),
            Image: new(item.Image),
            CreatorName: item.CreatorName,
            Category: new(item.Category.Id.Value, item.Category.Name)
        );

    public static PostProductResponse ToPostProductResponse (this GetProductByIdDto dto)
        => new(
            Id: dto.Id,
            Name: dto.Name,
            Description: dto.Description,
            Price: new(dto.Price),
            Status: dto.Status,
            UploadDate: dto.UploadDate.ToString(DateFormatString),
            CamCoordinates: new(dto.Cad.CamCoordinates),
            PanCoordinates: new(dto.Cad.PanCoordinates),
            CreatorName: dto.CreatorName,
            Category: new() { Id = dto.Category.Id.Value, Name = dto.Category.Name }
        );
}
