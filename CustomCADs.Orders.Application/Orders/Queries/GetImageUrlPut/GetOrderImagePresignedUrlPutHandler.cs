using CustomCADs.Shared.Application.Storage;

namespace CustomCADs.Orders.Application.Orders.Queries.GetImageUrlPut;

public class GetOrderImagePresignedUrlPutHandler(IStorageService storage)
    : IQueryHandler<GetOrderImagePresignedUrlPutQuery, GetOrderImagePresignedUrlPutDto>
{
    public async Task<GetOrderImagePresignedUrlPutDto> Handle(GetOrderImagePresignedUrlPutQuery req, CancellationToken ct)
    {
        string url = await storage.GetPresignedPutUrlAsync(
            key: req.ImageKey,
            contentType: req.ContentType,
            fileName: req.FileName
        ).ConfigureAwait(false);

        GetOrderImagePresignedUrlPutDto response = new(url);
        return response;
    }
}
