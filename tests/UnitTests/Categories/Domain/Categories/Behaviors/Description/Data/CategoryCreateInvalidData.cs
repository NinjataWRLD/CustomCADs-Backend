namespace CustomCADs.UnitTests.Categories.Domain.Categories.Behaviors.Description.Data;

using static CategoriesData;

public class CategoryCreateInvalidData : CategoryDescriptionData
{
	public CategoryCreateInvalidData()
	{
		Add(InvalidDescription);
		Add(MinInvalidDescription);
		Add(MaxInvalidDescription);
	}
}
