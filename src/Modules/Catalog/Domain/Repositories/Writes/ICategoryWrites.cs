using CustomCADs.Catalog.Domain.Categories;

namespace CustomCADs.Catalog.Domain.Repositories.Writes;

public interface ICategoryWrites
{
	Task<Category> AddAsync(Category entity, CancellationToken ct = default);
	void Remove(Category entity);
}
