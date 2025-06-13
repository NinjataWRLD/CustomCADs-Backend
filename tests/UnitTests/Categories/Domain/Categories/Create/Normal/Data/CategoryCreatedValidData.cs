namespace CustomCADs.UnitTests.Categories.Domain.Categories.Create.Normal.Data;

using static CategoriesData;

public class CategoryCreatedValidData : CategoryCreateData
{
	public CategoryCreatedValidData()
	{
		Add(ValidName, ValidDescription);
		Add(MinValidName, MinValidDescription);
		Add(MaxValidName, MaxValidDescription);
	}
}
