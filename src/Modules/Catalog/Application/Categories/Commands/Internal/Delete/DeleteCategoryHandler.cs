using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Catalog.Domain.Repositories.Writes;

namespace CustomCADs.Catalog.Application.Categories.Commands.Internal.Delete;

public sealed class DeleteCategoryHandler(ICategoryReads reads, ICategoryWrites writes, IUnitOfWork uow, BaseCachingService<CategoryId, Category> cache)
	: ICommandHandler<DeleteCategoryCommand>
{
	public async Task Handle(DeleteCategoryCommand req, CancellationToken ct)
	{
		Category category = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Category>.ById(req.Id);

		writes.Remove(category);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await cache.ClearAsync(req.Id).ConfigureAwait(false);
	}
}
