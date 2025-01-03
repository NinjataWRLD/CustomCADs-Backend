using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.GetImageUrlGet;

public sealed class GetProductImagePresignedUrlGetHandler(IProductReads reads, IRequestSender sender)
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

        GetImagePresignedUrlGetByIdQuery query = new(product.ImageId);
        string url = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);
        
        return new(PresignedUrl: url);
    }
}
