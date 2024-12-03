﻿using CustomCADs.Catalog.Endpoints.Common.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.Get.Recent;

public sealed record RecentProductsResponse(
    Guid Id,
    string Name,
    string Status,
    string UploadDate,
    CategoryResponse Category
);
