namespace CustomCADs.UnitTests.Categories.Domain.Categories.Properties.Description.Data;

using CustomCADs.UnitTests.Categories.Domain.Categories.Properties.Description;
using static CategoriesData;

public class CategoryCreateValidData : CategoryDescriptionData
{
    public CategoryCreateValidData()
    {
        Add(ValidDescription1);
        Add(ValidDescription2);
        Add(ValidDescription3);
    }
}
