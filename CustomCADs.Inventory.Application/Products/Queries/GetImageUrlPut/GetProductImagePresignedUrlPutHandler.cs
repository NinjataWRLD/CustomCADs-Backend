using CustomCADs.Inventory.Application.Products.Exceptions;
using CustomCADs.Inventory.Domain.Products;
using CustomCADs.Inventory.Domain.Products.Reads;
using CustomCADs.Shared.Application.Storage;

namespace CustomCADs.Inventory.Application.Products.Queries.GetImageUrlPut;

public class GetProductImagePresignedUrlPutHandler(IProductReads reads, IStorageService storage)
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

        string url = await storage.GetPresignedPutUrlAsync(
            key: product.Image.Key,
            contentType: req.ContentType,
            fileName: req.FileName
        ).ConfigureAwait(false);

        GetProductImagePresignedUrlPutDto response = new(PresignedUrl: url);
        return response;
    }
}
