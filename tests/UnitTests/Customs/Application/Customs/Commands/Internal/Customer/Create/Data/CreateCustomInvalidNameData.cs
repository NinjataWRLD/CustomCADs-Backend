namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Create.Data;

using static CustomsData;

public class CreateCustomInvalidNameData : CreateCustomData
{
	public CreateCustomInvalidNameData()
	{
		Add(InvalidName1, ValidDescription1, false);
		Add(InvalidName2, ValidDescription2, true);
	}
}
