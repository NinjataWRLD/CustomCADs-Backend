using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Get;

public sealed class CreatorGetProductImagePresignedUrlGetHandler(IProductReads reads, IRequestSender sender)
    : IQueryHandler<CreatorGetProductImagePresignedUrlGetQuery, CreatorGetProductImagePresignedUrlGetDto>
{
    public async Task<CreatorGetProductImagePresignedUrlGetDto> Handle(CreatorGetProductImagePresignedUrlGetQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Product>.ById(req.Id);

        if (product.CreatorId != req.CreatorId)
            throw CustomAuthorizationException<Product>.ById(req.CreatorId);

        GetImagePresignedUrlGetByIdQuery query = new(product.ImageId);
        var (Url, ContentType) = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        return new(
            PresignedUrl: Url,
            ContentType: ContentType
        );
    }
}
