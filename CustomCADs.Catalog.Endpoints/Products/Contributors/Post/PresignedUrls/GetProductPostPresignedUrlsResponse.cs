﻿namespace CustomCADs.Catalog.Endpoints.Products.Contributors.Post.PresignedUrls;

public sealed record GetProductPostPresignedUrlsResponse(
    string GeneratedImageKey,
    string PresignedImageUrl,
    string GeneratedCadKey,
    string PresignedCadUrl
);