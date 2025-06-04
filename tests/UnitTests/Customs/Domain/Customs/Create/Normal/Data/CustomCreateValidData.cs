namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create.Normal.Data;

using static CustomsData;

public class CustomCreateValidData : CustomCreateData
{
	public CustomCreateValidData()
	{
		Add(ValidName1, ValidDescription1, true);
		Add(ValidName2, ValidDescription2, false);
	}
}
