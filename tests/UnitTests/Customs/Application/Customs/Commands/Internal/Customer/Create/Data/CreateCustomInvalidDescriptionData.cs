namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Create.Data;

using static CustomsData;

public class CreateCustomInvalidDescriptionData : CreateCustomData
{
	public CreateCustomInvalidDescriptionData()
	{
		Add(ValidName1, InvalidDescription1, false);
		Add(ValidName2, InvalidDescription2, true);
	}
}
