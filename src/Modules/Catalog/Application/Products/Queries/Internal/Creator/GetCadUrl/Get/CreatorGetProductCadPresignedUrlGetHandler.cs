using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Get;

public sealed class CreatorGetProductCadPresignedUrlGetHandler(IProductReads reads, IRequestSender sender)
    : IQueryHandler<CreatorGetProductCadPresignedUrlGetQuery, CreatorGetProductCadPresignedUrlGetDto>
{
    public async Task<CreatorGetProductCadPresignedUrlGetDto> Handle(CreatorGetProductCadPresignedUrlGetQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Product>.ById(req.Id);

        if (product.CreatorId != req.CreatorId)
            throw CustomAuthorizationException<Product>.ById(product.Id);

        GetCadPresignedUrlGetByIdQuery query = new(product.CadId);
        var (Url, ContentType) = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        return new(
            PresignedUrl: Url,
            ContentType: ContentType
        );
    }
}
