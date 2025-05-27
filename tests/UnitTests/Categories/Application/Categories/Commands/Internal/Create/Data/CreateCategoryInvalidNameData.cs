namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Create.Data;

using CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Create;
using static CategoriesData;

public class CreateCategoryInvalidNameData : CreateCategoryData
{
	public CreateCategoryInvalidNameData()
	{
		Add(InvalidName1, ValidDescription1);
		Add(InvalidName2, ValidDescription2);
		Add(InvalidName3, ValidDescription3);
	}
}
