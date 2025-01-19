using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.GetCadUrlGet;

public sealed class GetProductCadPresignedUrlGetHandler(IProductReads reads, IRequestSender sender)
    : IQueryHandler<GetProductCadPresignedUrlGetQuery, GetProductCadPresignedUrlGetDto>
{
    public async Task<GetProductCadPresignedUrlGetDto> Handle(GetProductCadPresignedUrlGetQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        GetCadPresignedUrlGetByIdQuery query = new(product.CadId);
        string url = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        return new(PresignedUrl: url);
    }
}
