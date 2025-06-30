namespace CustomCADs.UnitTests.Categories.Domain.Categories.Create.Data;

using static CategoriesData;

public class CategoryCreateInvalidNameData : CategoryCreateData
{
	public CategoryCreateInvalidNameData()
	{
		Add(InvalidName, ValidDescription);
		Add(MinInvalidName, MinValidDescription);
		Add(MaxInvalidName, MaxValidDescription);
	}
}
