namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Create.Data;

using static CategoriesData;

public class CreateCategoryInvalidData : TheoryData<string, string>
{
	public CreateCategoryInvalidData()
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
