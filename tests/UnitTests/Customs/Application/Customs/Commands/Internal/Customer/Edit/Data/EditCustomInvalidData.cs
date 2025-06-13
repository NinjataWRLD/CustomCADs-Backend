namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Edit.Data;

using static CustomsData;

public class EditCustomInvalidData : TheoryData<string, string>
{
	public EditCustomInvalidData()
	{
		// Name
		Add(MinInvalidName, MinValidDescription);
		Add(MaxInvalidName, MaxValidDescription);

		// Description
		Add(MinValidName, MinInvalidDescription);
		Add(MaxValidName, MaxInvalidDescription);
	}
}
