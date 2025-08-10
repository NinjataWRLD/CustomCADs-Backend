namespace CustomCADs.UnitTests.Catalog.Domain.Categories.Create.Data;

using static CategoriesData;

public class CategoryCreateValidData : CategoryCreateData
{
	public CategoryCreateValidData()
	{
		Add(ValidName, ValidDescription);
		Add(MinValidName, MinValidDescription);
		Add(MaxValidName, MaxValidDescription);
	}
}
