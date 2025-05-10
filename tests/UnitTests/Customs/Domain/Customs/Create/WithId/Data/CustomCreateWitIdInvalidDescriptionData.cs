namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create.WithId.Data;

using CustomCADs.UnitTests.Customs.Domain.Customs.Create.WithId;
using static CustomsData;

public class CustomCreateWitIdInvalidDescriptionData : CustomCreateWithIdData
{
    public CustomCreateWitIdInvalidDescriptionData()
    {
        Add(ValidName1, InvalidDescription1, true);
        Add(ValidName2, InvalidDescription2, false);
    }
}
