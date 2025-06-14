namespace CustomCADs.UnitTests.Categories.Domain.Categories.Create.WithId.Data;

using static CategoriesData;

public class CategoryCreateWithIdInvalidNameData : CategoryCreateWithIdData
{
	public CategoryCreateWithIdInvalidNameData()
	{
		Add(InvalidName, ValidDescription);
		Add(MinInvalidName, MinValidDescription);
		Add(MaxInvalidName, MaxValidDescription);
	}
}
