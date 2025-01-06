using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.GetImageUrlPut;

public sealed class GetProductImagePresignedUrlPutHandler(IProductReads reads, IRequestSender sender)
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

        GetImagePresignedUrlPutByIdQuery query = new(
            Id: product.ImageId,
            NewContentType: req.ContentType,
            NewFileName: req.FileName
        );
        string url = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        return new(PresignedUrl: url);
    }
}
