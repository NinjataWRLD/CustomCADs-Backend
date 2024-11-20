﻿namespace CustomCADs.Catalog.Application.Products.Queries.GetImageUrlPut;

public record GetProductImagePresignedUrlPutQuery(
    string ImageKey,
    string ContentType,
    string FileName
) : IQuery<GetProductImagePresignedUrlPutDto>;
