using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Writes;
using CustomCADs.Catalog.Domain.Tags;

namespace CustomCADs.Catalog.Application.Tags.Commands.Internal.Create;

public class CreateTagHandler(ITagWrites writes, IUnitOfWork uow, BaseCachingService<TagId, Tag> cache)
	: ICommandHandler<CreateTagCommand, TagId>
{
	public async Task<TagId> Handle(CreateTagCommand req, CancellationToken ct)
	{
		Tag tag = await writes.AddAsync(Tag.Create(req.Name), ct).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await cache.UpdateAsync(tag.Id, tag).ConfigureAwait(false);
		return tag.Id;
	}
}
