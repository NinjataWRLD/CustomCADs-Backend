﻿namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetUrlGet.Image;

public sealed record GalleryGetProductImagePresignedUrlGetQuery(
    ProductId Id
) : IQuery<GalleryGetProductImagePresignedUrlGetDto>;
