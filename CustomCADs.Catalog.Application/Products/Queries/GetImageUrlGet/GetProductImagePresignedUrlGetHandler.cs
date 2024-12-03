using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Application.Storage;

namespace CustomCADs.Catalog.Application.Products.Queries.GetImageUrlGet;

public sealed class GetProductImagePresignedUrlGetHandler(IProductReads reads, IStorageService storage)
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
