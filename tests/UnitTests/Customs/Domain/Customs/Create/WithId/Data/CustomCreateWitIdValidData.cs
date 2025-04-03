namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create.WithId.Data;

using CustomCADs.UnitTests.Customs.Domain.Customs.Create.WithId;
using static CustomsData;

public class CustomCreateWitIdValidData : CustomCreateWithIdData
{
    public CustomCreateWitIdValidData()
    {
        Add(ValidId1, ValidName1, ValidDescription1, true, ValidBuyerId1);
        Add(ValidId2, ValidName2, ValidDescription2, false, ValidBuyerId2);
    }
}
