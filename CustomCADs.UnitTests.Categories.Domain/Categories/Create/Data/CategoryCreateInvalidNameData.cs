namespace CustomCADs.UnitTests.Categories.Domain.Categories.Create.Data;

using static CategoriesData;

public class CategoryCreateInvalidNameData : RoleCreateData
{
    public CategoryCreateInvalidNameData()
    {
        Add(InvalidName1, ValidDescription1);
        Add(InvalidName2, ValidDescription2);
        Add(InvalidName3, ValidDescription3);
    }
}
