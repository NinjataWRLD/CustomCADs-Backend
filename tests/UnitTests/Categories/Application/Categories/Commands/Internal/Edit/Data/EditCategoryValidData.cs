namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Edit.Data;

using CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Edit;
using static CategoriesData;

public class EditCategoryValidData : EditCategoryData
{
	public EditCategoryValidData()
	{
		Add(ValidName1, ValidDescription1);
		Add(ValidName2, ValidDescription2);
		Add(ValidName3, ValidDescription3);
	}
}
