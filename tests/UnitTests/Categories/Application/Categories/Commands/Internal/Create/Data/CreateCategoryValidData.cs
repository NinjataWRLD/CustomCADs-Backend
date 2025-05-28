namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Create.Data;

using CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Create;
using static CategoriesData;

public class CreateCategoryValidData : CreateCategoryData
{
	public CreateCategoryValidData()
	{
		Add(ValidName1, ValidDescription1);
		Add(ValidName2, ValidDescription2);
		Add(ValidName3, ValidDescription3);
	}
}
