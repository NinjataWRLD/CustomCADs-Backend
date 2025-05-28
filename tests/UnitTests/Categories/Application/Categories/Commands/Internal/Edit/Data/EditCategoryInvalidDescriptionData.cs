namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Edit.Data;

using CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Edit;
using static CategoriesData;

public class EditCategoryInvalidDescriptionData : EditCategoryData
{
	public EditCategoryInvalidDescriptionData()
	{
		Add(ValidName1, InvalidDescription1);
		Add(ValidName2, InvalidDescription2);
		Add(ValidName3, InvalidDescription3);
	}
}
