
using CustomCADs.Shared.Application.Storage;

namespace CustomCADs.Orders.Application.Orders.Queries.GetCadUrlPost;

public class GetOrderCadPresignedUrlPostHandler(IStorageService storage)
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

        GetOrderCadPresignedUrlPostDto response = new(CadKey, CadUrl);
        return new(
            PresignedKey: CadKey,
            GeneratedUrl: CadUrl
        );
    }
}
