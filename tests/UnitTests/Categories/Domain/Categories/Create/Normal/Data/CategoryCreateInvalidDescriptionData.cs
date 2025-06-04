namespace CustomCADs.UnitTests.Categories.Domain.Categories.Create.Normal.Data;

using static CategoriesData;

public class CategoryCreateInvalidDescriptionData : CategoryCreateData
{
	public CategoryCreateInvalidDescriptionData()
	{
		Add(ValidName1, InvalidDescription1);
		Add(ValidName2, InvalidDescription2);
		Add(ValidName3, InvalidDescription3);
	}
}
