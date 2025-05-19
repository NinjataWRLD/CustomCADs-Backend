namespace CustomCADs.UnitTests.Categories.Domain.Categories.Behaviors.Description.Data;

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
