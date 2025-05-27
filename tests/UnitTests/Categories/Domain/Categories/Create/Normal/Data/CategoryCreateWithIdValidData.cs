namespace CustomCADs.UnitTests.Categories.Domain.Categories.Create.Normal.Data;

using CustomCADs.UnitTests.Categories.Domain.Categories.Create.Normal;
using static CategoriesData;

public class CategoryCreateWithIdValidData : CategoryCreateData
{
	public CategoryCreateWithIdValidData()
	{
		Add(ValidName1, ValidDescription1);
		Add(ValidName2, ValidDescription2);
		Add(ValidName3, ValidDescription3);
	}
}
