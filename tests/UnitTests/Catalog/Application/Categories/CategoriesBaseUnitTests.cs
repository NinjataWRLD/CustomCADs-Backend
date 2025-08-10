namespace CustomCADs.UnitTests.Catalog.Application.Categories;

using static CategoriesData;

public class CategoriesBaseUnitTests
{
	public static readonly CancellationToken ct = CancellationToken.None;

	protected static Category CreateCategory(CategoryId? id = null, string name = ValidName, string description = ValidDescription)
		=> Category.CreateWithId(id ?? ValidId, name, description);
}
