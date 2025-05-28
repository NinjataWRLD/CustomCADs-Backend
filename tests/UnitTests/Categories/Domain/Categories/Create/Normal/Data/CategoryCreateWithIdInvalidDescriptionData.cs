namespace CustomCADs.UnitTests.Categories.Domain.Categories.Create.Normal.Data;

using CustomCADs.UnitTests.Categories.Domain.Categories.Create.Normal;
using static CategoriesData;

public class CategoryCreateWithIdInvalidDescriptionData : CategoryCreateData
{
	public CategoryCreateWithIdInvalidDescriptionData()
	{
		Add(ValidName1, InvalidDescription1);
		Add(ValidName2, InvalidDescription2);
		Add(ValidName3, InvalidDescription3);
	}
}
