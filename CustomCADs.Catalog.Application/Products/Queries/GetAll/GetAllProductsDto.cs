﻿using CustomCADs.Catalog.Application.Categories.Queries;
using CustomCADs.Shared.Core.Domain.ValueObjects;

namespace CustomCADs.Catalog.Application.Products.Queries.GetAll;

public record GetAllProductsDto(int Count, ICollection<GetAllProductsItem> Products);

public record GetAllProductsItem(
    ProductId Id,
    string Name,
    string Status,
    DateTime UploadDate,
    Image Image,
    string CreatorName,
    CategoryReadDto Category
)
{
    public GetAllProductsItem(Product product, string username, string categoryName) : this(
        Id: product.Id,
        Name: product.Name,
        Status: product.Status.ToString(),
        UploadDate: product.UploadDate,
        Image: product.Image,
        Category: new(product.CategoryId, categoryName),
        CreatorName: username
    )
    { }
}
