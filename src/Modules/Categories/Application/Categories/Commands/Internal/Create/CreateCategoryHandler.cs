using CustomCADs.Categories.Domain.Repositories;

namespace CustomCADs.Categories.Application.Categories.Commands.Internal.Create;

public sealed class CreateCategoryHandler(IWrites<Category> writes, IUnitOfWork uow, BaseCachingService<CategoryId, Category> cache)
	: ICommandHandler<CreateCategoryCommand, CategoryId>
{
	public async Task<CategoryId> Handle(CreateCategoryCommand req, CancellationToken ct)
	{
		var category = req.Dto.ToEntity();

		await writes.AddAsync(category, ct).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await cache.UpdateAsync(category.Id, category).ConfigureAwait(false);

		return category.Id;
	}
}
