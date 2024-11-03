﻿using CustomCADs.Catalog.Application.Categories.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.GetAll;

public record GetAllProductsDto(int Count, ICollection<GetAllProductsItem> Products);

public record GetAllProductsItem(
    Guid Id,
    string Name,
    string Status,
    DateTime UploadDate,
    string ImagePath,
    string CreatorName,
    CategoryReadDto Category
);
