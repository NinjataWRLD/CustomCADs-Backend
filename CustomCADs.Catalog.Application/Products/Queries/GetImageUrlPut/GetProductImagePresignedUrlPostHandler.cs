using CustomCADs.Shared.Application.Storage;

namespace CustomCADs.Catalog.Application.Products.Queries.GetImageUrlPut;

public class GetProductImagePresignedUrlPostHandler(IStorageService storage)
    : IQueryHandler<GetProductImagePresignedUrlPutQuery, GetProductImagePresignedUrlPutDto>
{
    public async Task<GetProductImagePresignedUrlPutDto> Handle(GetProductImagePresignedUrlPutQuery req, CancellationToken cancellationToken)
    {
        string url = await storage.GetPresignedPutUrlAsync(
            key: req.ImageKey,
            contentType: req.ContentType,
            fileName: req.FileName
        ).ConfigureAwait(false);

        GetProductImagePresignedUrlPutDto response = new(PresignedUrl: url);
        return response;
    }
}
