using CustomCADs.Catalog.Application.Tags.Dtos;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Catalog.Domain.Tags;

namespace CustomCADs.Catalog.Application.Tags.Queries.Internal.GetAll;

public class GetAllTagsHandler(ITagReads reads, BaseCachingService<TagId, Tag> cache)
	: IQueryHandler<GetAllTagsQuery, TagReadDto[]>
{
	public async Task<TagReadDto[]> Handle(GetAllTagsQuery req, CancellationToken ct)
	{
		Tag[] tags = [.. await cache.GetOrCreateAsync(
			factory: async () => await reads.AllAsync(track: false, ct: ct).ConfigureAwait(false)
		).ConfigureAwait(false)];

		return [.. tags.Select(x => x.ToDto())];
	}
}
