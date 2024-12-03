using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Application.Storage;

namespace CustomCADs.Catalog.Application.Products.Queries.GetImageUrlPut;

public sealed class GetProductImagePresignedUrlPutHandler(IProductReads reads, IStorageService storage)
    : IQueryHandler<GetProductImagePresignedUrlPutQuery, GetProductImagePresignedUrlPutDto>
{
    public async Task<GetProductImagePresignedUrlPutDto> Handle(GetProductImagePresignedUrlPutQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        if (product.CreatorId != req.CreatorId)
        {
            throw ProductAuthorizationException.ByProductId(req.Id);
        }

        string Url = await storage.GetPresignedPutUrlAsync(
            key: product.Image.Key,
            contentType: req.ContentType,
            fileName: req.FileName
        ).ConfigureAwait(false);

        return new(PresignedUrl: Url);
    }
}
