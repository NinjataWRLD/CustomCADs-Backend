namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create.WithId.Data;

using CustomCADs.UnitTests.Customs.Domain.Customs.Create.WithId;
using static CustomsData;

public class CustomCreateWitIdInvalidNameData : CustomCreateWithIdData
{
	public CustomCreateWitIdInvalidNameData()
	{
		Add(ValidId1, InvalidName1, ValidDescription1, true, ValidBuyerId1);
		Add(ValidId2, InvalidName2, ValidDescription2, false, ValidBuyerId2);
	}
}
