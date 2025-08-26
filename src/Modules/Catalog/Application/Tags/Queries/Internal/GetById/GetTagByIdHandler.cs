using CustomCADs.Catalog.Application.Tags.Dtos;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Catalog.Domain.Tags;

namespace CustomCADs.Catalog.Application.Tags.Queries.Internal.GetById;

public class GetTagByIdHandler(ITagReads reads, BaseCachingService<TagId, Tag> cache)
	: IQueryHandler<GetTagByIdQuery, TagReadDto>
{
	public async Task<TagReadDto> Handle(GetTagByIdQuery req, CancellationToken ct)
	{
		Tag tag = await cache.GetOrCreateAsync(
			id: req.Id,
			factory: async () => await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Tag>.ById(req.Id)
		).ConfigureAwait(false);

		return tag.ToDto();
	}
}
