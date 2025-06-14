namespace CustomCADs.UnitTests.Categories.Domain.Categories.Create.WithId.Data;

using static CategoriesData;

public class CategoryCreateWithIdInvalidDescriptionData : CategoryCreateWithIdData
{
	public CategoryCreateWithIdInvalidDescriptionData()
	{
		Add(ValidName, InvalidDescription);
		Add(MinValidName, MinInvalidDescription);
		Add(MaxValidName, MaxInvalidDescription);
	}
}
