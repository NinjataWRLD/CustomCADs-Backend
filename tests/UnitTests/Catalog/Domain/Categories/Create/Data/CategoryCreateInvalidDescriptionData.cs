namespace CustomCADs.UnitTests.Catalog.Domain.Categories.Create.Data;

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
