using CustomCADs.Shared.Application.Storage;

namespace CustomCADs.Orders.Application.Orders.Queries.GetImageUrl;

public class GetOrderImagePresignedUrlHandler(IStorageService storage)
    : IQueryHandler<GetOrderImagePresignedUrlQuery, GetOrderImagePresignedUrlDto>
{
    public async Task<GetOrderImagePresignedUrlDto> Handle(GetOrderImagePresignedUrlQuery req, CancellationToken ct)
    {
        (string key, string url) = await storage.GetPresignedPostUrlAsync(
            folderPath: "images",
            name: req.OrderName,
            contentType: req.ContentType,
            fileName: req.FileName
        ).ConfigureAwait(false);

        GetOrderImagePresignedUrlDto response = new(ImageKey: key, ImageUrl: url);
        return response;
    }
}
