using CustomCADs.Shared.Application.Storage;

namespace CustomCADs.Orders.Application.Orders.Queries.GetImageUrlGet;

public class GetOrderImagePresignedUrlGetHandler(IStorageService storage)
    : IQueryHandler<GetOrderImagePresignedUrlGetQuery, GetOrderImagePresignedUrlGetDto>
{
    public async Task<GetOrderImagePresignedUrlGetDto> Handle(GetOrderImagePresignedUrlGetQuery req, CancellationToken ct)
    {
        string imageUrl = await storage.GetPresignedGetUrlAsync(
            key: req.ImageKey,
            contentType: req.ImageContentType
        ).ConfigureAwait(false);

        GetOrderImagePresignedUrlGetDto response = new(imageUrl);
        return response;
    }
}
