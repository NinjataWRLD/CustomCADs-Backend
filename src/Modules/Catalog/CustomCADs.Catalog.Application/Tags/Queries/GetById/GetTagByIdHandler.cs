using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Tags;
using CustomCADs.Catalog.Domain.Tags.Reads;

namespace CustomCADs.Catalog.Application.Tags.Queries.GetById;

public class GetTagByIdHandler(ITagReads reads)
    : IQueryHandler<GetTagByIdQuery, GetTagByIdDto>
{
    public async Task<GetTagByIdDto> Handle(GetTagByIdQuery req, CancellationToken ct)
    {
        Tag tag = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw TagNotFoundException.ById(req.Id);

        return tag.ToGetTagByIdDto();
    }
}
