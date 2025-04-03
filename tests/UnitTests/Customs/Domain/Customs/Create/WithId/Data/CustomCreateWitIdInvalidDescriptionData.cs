namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create.WithId.Data;

using CustomCADs.UnitTests.Customs.Domain.Customs.Create.WithId;
using static CustomsData;

public class CustomCreateWitIdInvalidDescriptionData : CustomCreateWithIdData
{
    public CustomCreateWitIdInvalidDescriptionData()
    {
        Add(ValidId1, ValidName1, InvalidDescription1, true, ValidBuyerId1);
        Add(ValidId2, ValidName2, InvalidDescription2, false, ValidBuyerId2);
    }
}
