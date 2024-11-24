using CustomCADs.Inventory.Application.Products;
using CustomCADs.Inventory.Application.Products.Queries.GetAll;
using CustomCADs.Inventory.Application.Products.Queries.GetById;
using CustomCADs.Inventory.Endpoints.Products.Get.All;
using CustomCADs.Inventory.Endpoints.Products.Get.Recent;
using CustomCADs.Inventory.Endpoints.Products.Get.Single;
using CustomCADs.Inventory.Endpoints.Products.Post;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Inventory.Endpoints.Products;

using static Constants;

public static class Mapper
{
    public static RecentProductsResponse ToRecentProductsResponse(this GetAllProductsItem product)
        => new(
            Id: product.Id.Value,
            Name: product.Name,
            Status: product.Status,
            UploadDate: product.UploadDate.ToString(DateFormatString),
            Category: new(product.Category.Id.Value, product.Category.Name)
        );

    public static GetProductsDto ToGetProductsDto(this GetAllProductsItem product)
        => new(
            Id: product.Id.Value,
            Name: product.Name,
            UploadDate: product.UploadDate.ToString(DateFormatString),
            Image: new(product.Image.Key, product.Image.ContentType),
            CreatorName: product.CreatorName,
            Category: new(product.Category.Id.Value, product.Category.Name)
        );

    public static GetProductResponse ToGetProductResponse(this GetProductByIdDto product)
        => new(
            Id: product.Id.Value,
            Name: product.Name,
            Price: product.Price.ToMoneyDto(),
            Description: product.Description,
            UploadDate: product.UploadDate.ToString(DateFormatString),
            Category: new(product.Category.Id.Value, product.Category.Name),
            CamCoordinates: product.Cad.CamCoordinates.ToCoordinatesDto(),
            PanCoordinates: product.Cad.PanCoordinates.ToCoordinatesDto(),
            CadKey: product.Cad.Key
        );

    public static PostProductResponse ToPostProductResponse(this GetProductByIdDto product)
        => new(
            Id: product.Id.Value,
            Name: product.Name,
            Description: product.Description,
            Price: product.Price.ToMoneyDto(),
            Status: product.Status,
            UploadDate: product.UploadDate.ToString(DateFormatString),
            CreatorName: product.CreatorName,
            Category: new(product.Category.Id.Value, product.Category.Name)
        );

    public static MoneyDto ToMoneyDto(this Money dto)
        => new(
            Amount: dto.Amount,
            Currency: dto.Currency,
            Precision: dto.Precision,
            Symbol: dto.Symbol
        );
}
