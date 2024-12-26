using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Application.Storage;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.GetImageUrlGet;

public sealed class GetProductImagePresignedUrlGetHandler(IProductReads reads, IStorageService storage, IRequestSender sender)
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

        GetImageByIdQuery imageQuery = new(product.ImageId);
        var image = await sender.SendQueryAsync(imageQuery, ct).ConfigureAwait(false);

        string imageUrl = await storage.GetPresignedGetUrlAsync(
            key: image.Key,
            contentType: image.ContentType
        ).ConfigureAwait(false);

        return new(PresignedUrl: imageUrl);
    }
}
