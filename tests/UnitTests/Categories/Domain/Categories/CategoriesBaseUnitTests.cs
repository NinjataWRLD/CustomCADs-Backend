namespace CustomCADs.UnitTests.Categories.Domain.Categories;

using static CategoriesData;

public class CategoriesBaseUnitTests
{
	protected static Category CreateCategory(string name = ValidName, string description = ValidDescription)
		=> Category.Create(name, description);
}
