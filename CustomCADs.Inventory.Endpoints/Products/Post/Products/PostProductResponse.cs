﻿using CustomCADs.Inventory.Endpoints.Helpers.Dtos;

namespace CustomCADs.Inventory.Endpoints.Products.Post.Products;

public sealed record PostProductResponse(
    Guid Id,
    string Name,
    string Description,
    string CreatorName,
    string UploadDate,
    decimal Price,
    string Status,
    CategoryResponse Category
);