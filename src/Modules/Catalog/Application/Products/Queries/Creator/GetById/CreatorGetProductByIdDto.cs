﻿using CustomCADs.Catalog.Endpoints.Common.Dtos;

namespace CustomCADs.Catalog.Application.Products.Queries.Creator.GetById;

public record CreatorGetProductByIdDto(
    ProductId Id,
    string Name,
    string Description,
    decimal Price,
    string Status,
    DateTime UploadDate,
    CountsDto Counts,
    string CreatorName,
    CategoryDto Category
);

