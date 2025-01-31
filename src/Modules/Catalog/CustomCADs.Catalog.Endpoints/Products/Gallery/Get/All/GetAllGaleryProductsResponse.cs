﻿namespace CustomCADs.Catalog.Endpoints.Products.Gallery.Get.All;

public sealed record GetAllGaleryProductsResponse(
    Guid Id,
    string Name,
    string Category,
    int Views
);
