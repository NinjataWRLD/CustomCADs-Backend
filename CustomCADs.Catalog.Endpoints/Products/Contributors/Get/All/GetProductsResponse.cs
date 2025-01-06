﻿namespace CustomCADs.Catalog.Endpoints.Products.Contributors.Get.All;

public sealed record GetProductsResponse(
    Guid Id,
    string Name,
    string CreatorName,
    string UploadDate,
    Guid ImageId,
    CategoryResponse Category
);
