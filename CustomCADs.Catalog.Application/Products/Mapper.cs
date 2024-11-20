using CustomCADs.Catalog.Application.Products.Queries;
using CustomCADs.Catalog.Application.Products.Queries.GetAll;
using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Domain.Products.Entities;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Catalog.Application.Products;

public static class Mapper
{
    public static GetAllProductsItem ToGetAllProductsItem(this Product product, string username, string categoryName)
        => new(
            Id: product.Id,
            Name: product.Name,
            Status: product.Status.ToString(),
            UploadDate: product.UploadDate,
            Image: product.Image,
            Category: new(product.CategoryId, categoryName),
            CreatorName: username
        );

    public static GetProductByIdDto ToGetProductByIdDto(this Product product, CadDto cad, string username, string categoryName) 
        => new(
            Id: product.Id,
            Name: product.Name,
            Description: product.Description,
            Price: product.Price,
            UploadDate: product.UploadDate,
            Status: product.Status.ToString(),
            Image: product.Image,
            Cad: cad,
            Category: new(product.CategoryId, categoryName),
            CreatorName: username
        );

    public static Coordinates ToCoordinates(this CoordinatesDto coordinates)
        => new(coordinates.X, coordinates.Y, coordinates.Z);

    public static CoordinatesDto ToCoordinatesDto(this Coordinates coordinates)
        => new(coordinates.X, coordinates.Y, coordinates.Z);
}
