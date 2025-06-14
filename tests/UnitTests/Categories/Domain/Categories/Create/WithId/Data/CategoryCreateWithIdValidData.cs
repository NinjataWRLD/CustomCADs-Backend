namespace CustomCADs.UnitTests.Categories.Domain.Categories.Create.WithId.Data;

using static CategoriesData;

public class CategoryCreateWithIdValidData : CategoryCreateWithIdData
{
	public CategoryCreateWithIdValidData()
	{
		Add(ValidName, ValidDescription);
		Add(MinValidName, MinValidDescription);
		Add(MaxValidName, MaxValidDescription);
	}
}
