namespace CustomCADs.UnitTests.Categories.Domain.Categories.Create.WithId.Data;

using static CategoriesData;

public class CategoryCreateWithIdInvalidNameData : CategoryCreateWithIdData
{
	public CategoryCreateWithIdInvalidNameData()
	{
		Add(InvalidName1, ValidDescription1);
		Add(InvalidName2, ValidDescription2);
		Add(InvalidName3, ValidDescription3);
	}
}
