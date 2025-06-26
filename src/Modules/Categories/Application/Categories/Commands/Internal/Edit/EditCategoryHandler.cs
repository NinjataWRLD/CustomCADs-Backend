using CustomCADs.Categories.Domain.Repositories;
using CustomCADs.Categories.Domain.Repositories.Reads;

namespace CustomCADs.Categories.Application.Categories.Commands.Internal.Edit;

public sealed class EditCategoryHandler(ICategoryReads reads, IUnitOfWork uow, BaseCachingService<CategoryId, Category> cache)
	: ICommandHandler<EditCategoryCommand>
{
	public async Task Handle(EditCategoryCommand req, CancellationToken ct)
	{
		Category category = await cache.GetOrCreateAsync(
			id: req.Id,
			factory: async () => await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Category>.ById(req.Id)
		).ConfigureAwait(false);

		category.SetName(req.Dto.Name);
		category.SetDescription(req.Dto.Description);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await cache.UpdateAsync(category.Id, category).ConfigureAwait(false);
	}
}
