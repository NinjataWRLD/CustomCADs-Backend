namespace CustomCADs.UnitTests.Categories.Domain.Categories.Create.Normal.Data;

using static CategoriesData;

public class CategoryCreateInvalidNameData : CategoryCreateData
{
	public CategoryCreateInvalidNameData()
	{
		Add(InvalidName1, ValidDescription1);
		Add(InvalidName2, ValidDescription2);
		Add(InvalidName3, ValidDescription3);
	}
}
