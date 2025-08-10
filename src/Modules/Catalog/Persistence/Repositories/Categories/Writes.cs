using CustomCADs.Catalog.Domain.Categories;
using CustomCADs.Catalog.Domain.Repositories.Writes;

namespace CustomCADs.Catalog.Persistence.Repositories.Categories;

public class Writes(CatalogContext context) : ICategoryWrites
{
	public async Task<Category> AddAsync(Category category, CancellationToken ct = default)
		=> (await context.Categories.AddAsync(category, ct).ConfigureAwait(false)).Entity;

	public void Remove(Category category)
		=> context.Categories.Remove(category);
}
