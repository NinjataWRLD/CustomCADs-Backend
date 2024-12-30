namespace CustomCADs.UnitTests.Categories.Domain.Categories.Create.Data;

using static CategoriesData;

public class CategoryCreateInvalidDescriptionData : RoleCreateData
{
    public CategoryCreateInvalidDescriptionData()
    {
        Add(ValidName1, InvalidDescription1);
        Add(ValidName2, InvalidDescription2);
        Add(ValidName3, InvalidDescription3);
    }
}
