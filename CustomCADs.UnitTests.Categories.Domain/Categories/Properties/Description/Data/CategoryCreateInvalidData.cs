namespace CustomCADs.UnitTests.Categories.Domain.Categories.Properties.Description.Data;

using static CategoriesData;

public class CategoryCreateInvalidData : CategoryDescriptionData
{
    public CategoryCreateInvalidData()
    {
        Add(InvalidDescription1);
        Add(InvalidDescription2);
        Add(InvalidDescription3);
    }
}
