using CustomCADs.Shared.Application.Storage;

namespace CustomCADs.Inventory.Application.Products.Queries.GetCadUrlPost;

public sealed class GetProductCadPresignedUrlPostHandler(IStorageService storage)
    : IQueryHandler<GetProductCadPresignedUrlPostQuery, GetProductCadPresignedUrlPostDto>
{
    public async Task<GetProductCadPresignedUrlPostDto> Handle(GetProductCadPresignedUrlPostQuery req, CancellationToken cancellationToken)
    {
        (string Key, string Url) = await storage.GetPresignedPostUrlAsync(
            folderPath: "cads",
            name: req.ProductName,
            contentType: req.ContentType,
            fileName: req.FileName
        ).ConfigureAwait(false);

        return new(CadKey: Key, CadUrl: Url);
    }
}
