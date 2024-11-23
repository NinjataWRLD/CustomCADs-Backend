using CustomCADs.Inventory.Application.Products.Queries.GetById;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Application.Storage;

namespace CustomCADs.Inventory.Application.Products.Queries.GetCadUrlGet;

public class GetProductCadPresignedUrlGetHandler(IStorageService storage, IRequestSender sender)
    : IQueryHandler<GetProductCadPresignedUrlGetQuery, GetProductCadPresignedUrlGetDto>
{
    public async Task<GetProductCadPresignedUrlGetDto> Handle(GetProductCadPresignedUrlGetQuery req, CancellationToken ct)
    {
        GetProductByIdQuery query = new(req.Id);
        GetProductByIdDto product = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        string cadUrl = await storage.GetPresignedGetUrlAsync(
            key: product.Cad.Key,
            contentType: product.Cad.ContentType
        ).ConfigureAwait(false);

        GetProductCadPresignedUrlGetDto response = new(cadUrl);
        return response;
    }
}
