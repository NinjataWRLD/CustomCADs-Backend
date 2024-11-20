using CustomCADs.Shared.Application.Storage;

namespace CustomCADs.Catalog.Application.Products.Queries.GetCadUrl;

public class GetProductCadPresignedUrlHandler(IStorageService storage)
    : IQueryHandler<GetProductCadPresignedUrlQuery, GetProductCadPresignedUrlDto>
{
    public async Task<GetProductCadPresignedUrlDto> Handle(GetProductCadPresignedUrlQuery req, CancellationToken cancellationToken)
    {
        (string key, string url) = await storage.GetPresignedPostUrlAsync(
            folderPath: "cads",
            name: req.ProductName,
            contentType: req.ContentType,
            fileName: req.FileName
        ).ConfigureAwait(false);

        GetProductCadPresignedUrlDto response = new(CadKey: key, CadUrl: url);
        return response;
    }
}
