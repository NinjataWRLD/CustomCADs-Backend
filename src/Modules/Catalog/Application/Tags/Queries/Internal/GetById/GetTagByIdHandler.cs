using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Catalog.Domain.Tags;

namespace CustomCADs.Catalog.Application.Tags.Queries.Internal.GetById;

public class GetTagByIdHandler(ITagReads reads)
	: IQueryHandler<GetTagByIdQuery, GetTagByIdDto>
{
	public async Task<GetTagByIdDto> Handle(GetTagByIdQuery req, CancellationToken ct)
	{
		Tag tag = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Tag>.ById(req.Id);

		return tag.ToGetByIdDto();
	}
}
