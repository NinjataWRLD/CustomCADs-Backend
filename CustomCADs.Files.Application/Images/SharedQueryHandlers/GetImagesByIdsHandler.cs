using CustomCADs.Files.Domain.Images.Reads;
using CustomCADs.Shared.Application.Requests.Queries;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.Files.Application.Images.SharedQueryHandlers;

public class GetImagesByIdsHandler(IImageReads reads)
    : IQueryHandler<GetImagesByIdsQuery, (ImageId Id, string Key, string ContentType)[]>
{
    public async Task<(ImageId Id, string Key, string ContentType)[]> Handle(GetImagesByIdsQuery req, CancellationToken ct)
    {
        ImageQuery query = new(
            Ids: req.Ids,
            Pagination: new(Page: 1, Limit: req.Ids.Length)
        );
        Result<Image> result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

        return [.. result.Items.Select(i => (i.Id, i.Key, i.ContentType))];
    }
}
