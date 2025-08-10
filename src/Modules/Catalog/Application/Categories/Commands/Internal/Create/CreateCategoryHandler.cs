using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Writes;

namespace CustomCADs.Catalog.Application.Categories.Commands.Internal.Create;

public sealed class CreateCategoryHandler(ICategoryWrites writes, IUnitOfWork uow, BaseCachingService<CategoryId, Category> cache)
	: ICommandHandler<CreateCategoryCommand, CategoryId>
{
	public async Task<CategoryId> Handle(CreateCategoryCommand req, CancellationToken ct)
	{
		Category category = await writes.AddAsync(
			entity: Category.Create(req.Dto.Name, req.Dto.Description),
			ct
		).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await cache.UpdateAsync(category.Id, category).ConfigureAwait(false);

		return category.Id;
	}
}
