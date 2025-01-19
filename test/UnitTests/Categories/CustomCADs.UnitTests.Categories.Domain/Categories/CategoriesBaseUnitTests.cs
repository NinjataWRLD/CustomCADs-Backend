namespace CustomCADs.UnitTests.Categories.Domain.Categories;

using static CategoriesData;

public class CategoriesBaseUnitTests
{
    protected static Category CreateCategory(string name = ValidName1, string description = ValidDescription1)
        => Category.Create(name, description);
}
