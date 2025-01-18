using CustomCADs.Files.Application.Common.Exceptions;
using CustomCADs.Files.Domain.Images.Reads;
using CustomCADs.Shared.Abstractions.Requests.Queries;
using CustomCADs.Shared.Abstractions.Storage;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.Files.Application.Images.SharedQueryHandlers;

public class GetImagePresignedUrlGetByIdHandler(IImageReads reads, IStorageService storage)
    : IQueryHandler<GetImagePresignedUrlGetByIdQuery, string>
{
    public async Task<string> Handle(GetImagePresignedUrlGetByIdQuery req, CancellationToken ct)
    {
        Image image = await reads.SingleByIdAsync(req.Id, track: false, ct).ConfigureAwait(false)
            ?? throw ImageNotFoundException.ById(req.Id);

        string url = await storage.GetPresignedGetUrlAsync(
            key: image.Key,
            contentType: image.ContentType
        ).ConfigureAwait(false);

        return url;
    }
}
