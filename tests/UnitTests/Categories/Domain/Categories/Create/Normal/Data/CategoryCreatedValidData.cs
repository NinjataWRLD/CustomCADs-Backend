namespace CustomCADs.UnitTests.Categories.Domain.Categories.Create.Normal.Data;

using static CategoriesData;

public class CategoryCreatedValidData : CategoryCreateData
{
    public CategoryCreatedValidData()
    {
        Add(ValidName1, ValidDescription1);
        Add(ValidName2, ValidDescription2);
        Add(ValidName3, ValidDescription3);
    }
}
