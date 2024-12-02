﻿using CustomCADs.Inventory.Application.Common.Exceptions;
using CustomCADs.Inventory.Domain.Products;
using CustomCADs.Inventory.Domain.Products.Reads;
using CustomCADs.Shared.Application.Storage;

namespace CustomCADs.Inventory.Application.Products.Queries.GetImageUrlGet;

public class GetProductImagePresignedUrlGetHandler(IProductReads reads, IStorageService storage)
    : IQueryHandler<GetProductImagePresignedUrlGetQuery, GetProductImagePresignedUrlGetDto>
{
    public async Task<GetProductImagePresignedUrlGetDto> Handle(GetProductImagePresignedUrlGetQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        if (product.CreatorId != req.CreatorId)
        {
            throw ProductAuthorizationException.ByProductId(req.Id);
        }

        string imageUrl = await storage.GetPresignedGetUrlAsync(
            key: product.Image.Key,
            contentType: product.Image.ContentType
        ).ConfigureAwait(false);

        return new(PresignedUrl: imageUrl);
    }
}