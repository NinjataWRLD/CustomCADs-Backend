namespace CustomCADs.UnitTests.Categories.Domain.Categories.Create.Normal.Data;

using CustomCADs.UnitTests.Categories.Domain.Categories.Create.Normal;
using static CategoriesData;

public class CategoryCreateWithIdInvalidNameData : CategoryCreateData
{
    public CategoryCreateWithIdInvalidNameData()
    {
        Add(InvalidName1, ValidDescription1);
        Add(InvalidName2, ValidDescription2);
        Add(InvalidName3, ValidDescription3);
    }
}
