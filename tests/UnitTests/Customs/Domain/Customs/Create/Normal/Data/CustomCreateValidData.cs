namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create.Normal.Data;

using CustomCADs.UnitTests.Customs.Domain.Customs.Create.Normal;
using static CustomsData;

public class CustomCreateValidData : CustomCreateData
{
	public CustomCreateValidData()
	{
		Add(ValidName1, ValidDescription1, true, ValidBuyerId1);
		Add(ValidName2, ValidDescription2, false, ValidBuyerId2);
	}
}
