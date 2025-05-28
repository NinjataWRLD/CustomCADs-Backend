namespace CustomCADs.UnitTests.Categories.Domain.Categories.Create.WithId.Data;

using static CategoriesData;

public class CategoryCreateInvalidDescriptionData : CategoryCreateWithIdData
{
	public CategoryCreateInvalidDescriptionData()
	{
		Add(ValidName1, InvalidDescription1);
		Add(ValidName2, InvalidDescription2);
		Add(ValidName3, InvalidDescription3);
	}
}
