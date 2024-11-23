using CustomCADs.Shared.Application.Storage;

namespace CustomCADs.Inventory.Application.Products.Queries.GetImageUrlPost;

public class GetProductImagePresignedUrlPostHandler(IStorageService storage)
    : IQueryHandler<GetProductImagePresignedUrlPostQuery, GetProductImagePresignedUrlPostDto>
{
    public async Task<GetProductImagePresignedUrlPostDto> Handle(GetProductImagePresignedUrlPostQuery req, CancellationToken cancellationToken)
    {
        (string key, string url) = await storage.GetPresignedPostUrlAsync(
            folderPath: "images",
            name: req.ProductName,
            contentType: req.ContentType,
            fileName: req.FileName
        ).ConfigureAwait(false);

        GetProductImagePresignedUrlPostDto response = new(GeneratedKey: key, PresignedUrl: url);
        return response;
    }
}
