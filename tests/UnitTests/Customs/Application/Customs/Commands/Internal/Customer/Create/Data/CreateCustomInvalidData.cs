namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Create.Data;

using static CustomsData;

public class CreateCustomInvalidData : TheoryData<string, string, bool>
{
	public CreateCustomInvalidData()
	{
		// Name
		Add(MinInvalidName, MinValidDescription, false);
		Add(MaxInvalidName, MaxValidDescription, true);

		// Description
		Add(MinValidName, MinInvalidDescription, false);
		Add(MaxValidName, MaxInvalidDescription, true);
	}
}
