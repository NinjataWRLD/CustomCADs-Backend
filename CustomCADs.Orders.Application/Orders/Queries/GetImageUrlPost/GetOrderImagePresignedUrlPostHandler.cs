using CustomCADs.Shared.Application.Storage;

namespace CustomCADs.Orders.Application.Orders.Queries.GetImageUrlPost;

public class GetOrderImagePresignedUrlPostHandler(IStorageService storage)
    : IQueryHandler<GetOrderImagePresignedUrlPostQuery, GetOrderImagePresignedUrlPostDto>
{
    public async Task<GetOrderImagePresignedUrlPostDto> Handle(GetOrderImagePresignedUrlPostQuery req, CancellationToken ct)
    {
        (string key, string url) = await storage.GetPresignedPostUrlAsync(
            folderPath: "images",
            name: req.OrderName,
            contentType: req.ContentType,
            fileName: req.FileName
        ).ConfigureAwait(false);

        GetOrderImagePresignedUrlPostDto response = new(GeneratedKey: key, PresignedUrl: url);
        return response;
    }
}
