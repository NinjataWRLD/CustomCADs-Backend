using CustomCADs.Categories.Domain.Repositories;
using CustomCADs.Categories.Domain.Repositories.Reads;

namespace CustomCADs.Categories.Application.Categories.Commands.Internal.Delete;

public sealed class DeleteCategoryHandler(ICategoryReads reads, IWrites<Category> writes, IUnitOfWork uow, BaseCachingService<CategoryId, Category> cache)
	: ICommandHandler<DeleteCategoryCommand>
{
	public async Task Handle(DeleteCategoryCommand req, CancellationToken ct)
	{
		Category category = await cache.GetOrCreateAsync(
			id: req.Id,
			factory: async () => await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Category>.ById(req.Id)
		).ConfigureAwait(false);

		writes.Remove(category);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await cache.ClearAsync(req.Id).ConfigureAwait(false);
	}
}
