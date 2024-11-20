using CustomCADs.Shared.Application.Storage;

namespace CustomCADs.Catalog.Application.Products.Queries.GetCadUrlPost;

public class GetProductCadPresignedUrlPostHandler(IStorageService storage)
    : IQueryHandler<GetProductCadPresignedUrlPostQuery, GetProductCadPresignedUrlPostDto>
{
    public async Task<GetProductCadPresignedUrlPostDto> Handle(GetProductCadPresignedUrlPostQuery req, CancellationToken cancellationToken)
    {
        (string key, string url) = await storage.GetPresignedPostUrlAsync(
            folderPath: "cads",
            name: req.ProductName,
            contentType: req.ContentType,
            fileName: req.FileName
        ).ConfigureAwait(false);

        GetProductCadPresignedUrlPostDto response = new(CadKey: key, CadUrl: url);
        return response;
    }
}
