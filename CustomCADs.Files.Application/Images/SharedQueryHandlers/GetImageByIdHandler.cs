using CustomCADs.Files.Application.Common.Exceptions;
using CustomCADs.Files.Domain.Images;
using CustomCADs.Files.Domain.Images.Reads;
using CustomCADs.Shared.Application.Requests.Queries;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.Files.Application.Images.SharedQueryHandlers;

public sealed class GetImageByIdHandler(IImageReads reads)
    : IQueryHandler<GetImageByIdQuery, (ImageId Id, string Key, string ContentType)>
{
    public async Task<(ImageId Id, string Key, string ContentType)> Handle(GetImageByIdQuery req, CancellationToken ct)
    {
        Image image = await reads.SingleByIdAsync(req.Id, track: false, ct: ct)
            ?? throw ImageNotFoundException.ById(req.Id);

        return (Id: image.Id, Key: image.Key, ContentType: image.ContentType);
    }
}
