namespace CustomCADs.UnitTests.Categories.Domain.Categories.Create.Normal.Data;

using static CategoriesData;

public class CategoryCreateInvalidDescriptionData : CategoryCreateData
{
	public CategoryCreateInvalidDescriptionData()
	{
		Add(ValidName, InvalidDescription);
		Add(MinValidName, MinInvalidDescription);
		Add(MaxValidName, MaxInvalidDescription);
	}
}
