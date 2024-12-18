using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Application.Storage;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.GetImageUrlPut;

public sealed class GetProductImagePresignedUrlPutHandler(IProductReads reads, IStorageService storage, IRequestSender sender)
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

        GetImageByIdQuery imageQuery = new(product.ImageId);
        var (_, Key, _) = await sender.SendQueryAsync(imageQuery, ct).ConfigureAwait(false);

        string Url = await storage.GetPresignedPutUrlAsync(
            key: Key,
            contentType: req.ContentType,
            fileName: req.FileName
        ).ConfigureAwait(false);

        return new(PresignedUrl: Url);
    }
}
