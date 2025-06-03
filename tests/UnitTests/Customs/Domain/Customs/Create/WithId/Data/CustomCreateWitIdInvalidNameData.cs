namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create.WithId.Data;

using static CustomsData;

public class CustomCreateWitIdInvalidNameData : CustomCreateWithIdData
{
    public CustomCreateWitIdInvalidNameData()
    {
        Add(InvalidName1, ValidDescription1, true);
        Add(InvalidName2, ValidDescription2, false);
    }
}
