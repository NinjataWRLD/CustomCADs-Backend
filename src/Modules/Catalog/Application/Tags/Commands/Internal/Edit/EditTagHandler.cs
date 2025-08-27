using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Catalog.Domain.Tags;

namespace CustomCADs.Catalog.Application.Tags.Commands.Internal.Edit;

public class EditTagHandler(ITagReads reads, IUnitOfWork uow, BaseCachingService<TagId, Tag> cache)
	: ICommandHandler<EditTagCommand>
{
	public async Task Handle(EditTagCommand req, CancellationToken ct)
	{
		Tag tag = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Tag>.ById(req.Id);

		tag.SetName(req.Name);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await cache.UpdateAsync(tag.Id, tag).ConfigureAwait(false);
	}
}
