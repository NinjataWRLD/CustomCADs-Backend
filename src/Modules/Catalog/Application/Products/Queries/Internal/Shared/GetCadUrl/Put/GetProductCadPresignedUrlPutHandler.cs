using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Shared.GetCadUrl.Put;

public sealed class GetProductCadPresignedUrlPutHandler(IProductReads reads, IRequestSender sender)
    : IQueryHandler<GetProductCadPresignedUrlPutQuery, GetProductCadPresignedUrlPutDto>
{
    public async Task<GetProductCadPresignedUrlPutDto> Handle(GetProductCadPresignedUrlPutQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Product>.ById(req.Id);

        if (product.CreatorId != req.CreatorId)
        {
            throw CustomAuthorizationException<Product>.ById(req.Id);
        }

        GetCadPresignedUrlPutByIdQuery query = new(
            Id: product.CadId,
            NewContentType: req.ContentType,
            NewFileName: req.FileName
        );
        string url = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        return new(PresignedUrl: url);
    }
}
