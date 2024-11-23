using CustomCADs.Inventory.Application.Products.Queries.GetById;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Application.Storage;

namespace CustomCADs.Inventory.Application.Products.Queries.GetImageUrlGet;

public class GetProductImagePresignedUrlGetHandler(IStorageService storage, IRequestSender sender)
    : IQueryHandler<GetProductImagePresignedUrlGetQuery, GetProductImagePresignedUrlGetDto>
{
    public async Task<GetProductImagePresignedUrlGetDto> Handle(GetProductImagePresignedUrlGetQuery req, CancellationToken ct)
    {
        GetProductByIdQuery query = new(req.Id);
        GetProductByIdDto product = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        string imageUrl = await storage.GetPresignedGetUrlAsync(
            key: product.Image.Key,
            contentType: product.Image.ContentType
        ).ConfigureAwait(false);

        GetProductImagePresignedUrlGetDto response = new(imageUrl);
        return response;
    }
}
