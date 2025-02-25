using CustomCADs.Files.Application.Common.Exceptions;
using CustomCADs.Files.Domain.Images.Reads;
using CustomCADs.Shared.Abstractions.Requests.Queries;
using CustomCADs.Shared.Abstractions.Storage;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.Files.Application.Images.SharedQueryHandlers;

public class GetImagePresignedUrlPutByIdHandler(IImageReads reads, IStorageService storage)
    : IQueryHandler<GetImagePresignedUrlPutByIdQuery, string>
{
    public async Task<string> Handle(GetImagePresignedUrlPutByIdQuery req, CancellationToken ct)
    {
        Image image = await reads.SingleByIdAsync(req.Id, track: false, ct).ConfigureAwait(false)
            ?? throw ImageNotFoundException.ById(req.Id);

        string url = await storage.GetPresignedPutUrlAsync(
            key: image.Key,
            contentType: req.NewContentType,
            fileName: req.NewFileName
        ).ConfigureAwait(false);

        return url;
    }
}
