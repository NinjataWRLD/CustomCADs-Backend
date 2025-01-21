﻿namespace CustomCADs.Catalog.Endpoints.Products.Gallery.Get.Single;

public sealed record GetGalleryProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string UploadDate,
    CountsDto Counts,
    CategoryResponse Category
);
