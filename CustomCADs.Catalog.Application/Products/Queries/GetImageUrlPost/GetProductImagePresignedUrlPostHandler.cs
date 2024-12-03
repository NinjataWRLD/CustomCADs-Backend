using CustomCADs.Shared.Application.Storage;

namespace CustomCADs.Catalog.Application.Products.Queries.GetImageUrlPost;

public sealed class GetProductImagePresignedUrlPostHandler(IStorageService storage)
    : IQueryHandler<GetProductImagePresignedUrlPostQuery, GetProductImagePresignedUrlPostDto>
{
    public async Task<GetProductImagePresignedUrlPostDto> Handle(GetProductImagePresignedUrlPostQuery req, CancellationToken cancellationToken)
    {
        (string Key, string Url) = await storage.GetPresignedPostUrlAsync(
            folderPath: "images",
            name: req.ProductName,
            contentType: req.ContentType,
            fileName: req.FileName
        ).ConfigureAwait(false);

        return new(GeneratedKey: Key, PresignedUrl: Url);
    }
}
