using CustomCADs.Shared.Application.Storage;

namespace CustomCADs.Catalog.Application.Products.Queries.GetImageUrl;

public class GetProductImagePresignedUrlHandler(IStorageService storage)
    : IQueryHandler<GetProductImagePresignedUrlQuery, GetProductImagePresignedUrlDto>
{
    public async Task<GetProductImagePresignedUrlDto> Handle(GetProductImagePresignedUrlQuery req, CancellationToken cancellationToken)
    {
        (string key, string url) = await storage.GetPresignedPostUrlAsync(
            folderPath: "images",
            name: req.ProductName,
            contentType: req.ContentType,
            fileName: req.FileName
        ).ConfigureAwait(false);

        GetProductImagePresignedUrlDto response = new(ImageKey: key, ImageUrl: url);
        return response;
    }
}
