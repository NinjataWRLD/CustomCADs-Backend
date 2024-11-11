﻿using CustomCADs.Catalog.Application.Categories.Queries;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Deliveries.Digital;

namespace CustomCADs.Catalog.Application.Products.Queries.GetById;

public record GetProductByIdDto(
    ProductId Id,
    string Name,
    string Description,
    Money Price,
    string Status,
    Image Image,
    string CreatorName,
    Cad Cad,
    DateTime UploadDate,
    CategoryReadDto Category
)
{
    public GetProductByIdDto(Product product, string username) : this(
        Id: product.Id,
        Name: product.Name,
        Description: product.Description,
        Price: product.Price,
        UploadDate: product.UploadDate,
        Status: product.Status.ToString(),
        Image: product.Image,
        Cad: product.Cad,
        Category: new(product.Category.Id, product.Category.Name),
        CreatorName: username
    )
    { }
}

