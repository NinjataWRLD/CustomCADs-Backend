namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create.WithId.Data;

using static CustomsData;

public class CustomCreateWitIdValidData : CustomCreateWithIdData
{
	public CustomCreateWitIdValidData()
	{
		Add(ValidName1, ValidDescription1, true);
		Add(ValidName2, ValidDescription2, false);
	}
}
