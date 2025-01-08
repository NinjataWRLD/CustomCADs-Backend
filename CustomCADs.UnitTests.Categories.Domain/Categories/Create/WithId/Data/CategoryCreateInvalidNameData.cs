namespace CustomCADs.UnitTests.Categories.Domain.Categories.Create.WithId.Data;

using static CategoriesData;

public class CategoryCreateInvalidNameData : CategoryCreateWithIdData
{
    public CategoryCreateInvalidNameData()
    {
        Add(InvalidName1, ValidDescription1);
        Add(InvalidName2, ValidDescription2);
        Add(InvalidName3, ValidDescription3);
    }
}
