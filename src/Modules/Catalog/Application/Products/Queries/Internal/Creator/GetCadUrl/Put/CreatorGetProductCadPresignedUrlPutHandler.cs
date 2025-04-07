using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put;

public sealed class CreatorGetProductCadPresignedUrlPutHandler(IProductReads reads, IRequestSender sender)
    : IQueryHandler<CreatorGetProductCadPresignedUrlPutQuery, CreatorGetProductCadPresignedUrlPutDto>
{
    public async Task<CreatorGetProductCadPresignedUrlPutDto> Handle(CreatorGetProductCadPresignedUrlPutQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Product>.ById(req.Id);

        if (product.CreatorId != req.CreatorId)
        {
            throw CustomAuthorizationException<Product>.ById(req.Id);
        }

        GetCadPresignedUrlPutByIdQuery query = new(
            Id: product.CadId,
            NewFile: req.NewCad
        );
        string url = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        return new(PresignedUrl: url);
    }
}
