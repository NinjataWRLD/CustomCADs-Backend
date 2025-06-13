namespace CustomCADs.UnitTests.Categories.Domain.Categories.Behaviors.Description.Data;

using static CategoriesData;

public class CategoryCreateValidData : CategoryDescriptionData
{
	public CategoryCreateValidData()
	{
		Add(ValidDescription);
		Add(MinValidDescription);
		Add(MaxValidDescription);
	}
}
