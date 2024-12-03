using CustomCADs.Shared.Application.Storage;

namespace CustomCADs.Orders.Application.Orders.Queries.GetCadUrlPost;

public sealed class GetOrderCadPresignedUrlPostHandler(IStorageService storage)
    : IQueryHandler<GetOrderCadPresignedUrlPostQuery, GetOrderCadPresignedUrlPostDto>
{
    public async Task<GetOrderCadPresignedUrlPostDto> Handle(GetOrderCadPresignedUrlPostQuery req, CancellationToken ct)
    {
        var (CadKey, CadUrl) = await storage.GetPresignedPostUrlAsync(
            folderPath: "cads",
            name: req.OrderName,
            contentType: req.ContentType,
            fileName: req.FileName
        ).ConfigureAwait(false);

        return new(
            PresignedKey: CadKey,
            GeneratedUrl: CadUrl
        );
    }
}
