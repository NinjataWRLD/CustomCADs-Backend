﻿using CustomCADs.Inventory.Application.Products.Queries;
using CustomCADs.Inventory.Application.Products.Queries.GetAll;
using CustomCADs.Inventory.Application.Products.Queries.GetById;
using CustomCADs.Inventory.Domain.Products;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.ValueObjects;

namespace CustomCADs.Inventory.Application.Products;

public static class Mapper
{
    public static GetAllProductsDto ToGetAllProductsItem(this Product product, string username, string categoryName, string timeZone)
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

    public static GetProductByIdDto ToGetProductByIdDto(this Product product, CadDto cad, string username, string categoryName, string timeZone)
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
            Cad: cad,
            Category: (product.CategoryId, categoryName),
            CreatorName: username
        );

    public static Coordinates ToCoordinates(this CoordinatesDto coordinates)
        => new(coordinates.X, coordinates.Y, coordinates.Z);

    public static CoordinatesDto ToCoordinatesDto(this Coordinates coordinates)
        => new(coordinates.X, coordinates.Y, coordinates.Z);
}
