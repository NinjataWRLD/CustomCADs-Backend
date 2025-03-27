using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Queries;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.Files.Application.Images.Queries.Shared;

public class GetImagesByIdsHandler(IImageReads reads)
    : IQueryHandler<GetImagesByIdsQuery, Dictionary<ImageId, (string Key, string ContentType)>>
{
    public async Task<Dictionary<ImageId, (string Key, string ContentType)>> Handle(GetImagesByIdsQuery req, CancellationToken ct)
    {
        ImageQuery query = new(
            Ids: req.Ids,
            Pagination: new(Page: 1, Limit: req.Ids.Length)
        );
        Result<Image> result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

        return result.Items.ToDictionary(x => x.Id, x => (x.Key, x.ContentType));
    }
}
