using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Catalog.Domain.Tags;

namespace CustomCADs.Catalog.Application.Tags.Queries.Internal.GetAll;

public class GetAllTagsHandler(ITagReads reads)
    : IQueryHandler<GetAllTagsQuery, GetAllTagsDto[]>
{
    public async Task<GetAllTagsDto[]> Handle(GetAllTagsQuery req, CancellationToken ct)
    {
        Tag[] tags = await reads.AllAsync(track: false, ct: ct).ConfigureAwait(false);

        return [.. tags.Select(x => x.ToGetAllDto())];
    }
}
