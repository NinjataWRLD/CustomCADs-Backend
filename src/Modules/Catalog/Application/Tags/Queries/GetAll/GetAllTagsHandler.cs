﻿using CustomCADs.Catalog.Domain.Tags;
using CustomCADs.Catalog.Domain.Tags.Reads;

namespace CustomCADs.Catalog.Application.Tags.Queries.GetAll;

public class GetAllTagsHandler(ITagReads reads)
    : IQueryHandler<GetAllTagsQuery, GetAllTagsDto[]>
{
    public async Task<GetAllTagsDto[]> Handle(GetAllTagsQuery req, CancellationToken ct)
    {
        Tag[] tags = await reads.AllAsync(track: false, ct: ct).ConfigureAwait(false);

        return [.. tags.Select(x => x.ToGetAllTagsDto())];
    }
}
