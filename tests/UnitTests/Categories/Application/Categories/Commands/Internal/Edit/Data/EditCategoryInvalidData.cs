namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Edit.Data;

using static CategoriesData;

public class EditCategoryInvalidData : TheoryData<string, string>
{
	public EditCategoryInvalidData()
	{
		// Name
		Add(InvalidName, ValidDescription);
		Add(MinInvalidName, MinValidDescription);
		Add(MaxInvalidName, MaxValidDescription);

		// Description
		Add(ValidName, InvalidDescription);
		Add(MinValidName, MinInvalidDescription);
		Add(MaxValidName, MaxInvalidDescription);
	}
}
