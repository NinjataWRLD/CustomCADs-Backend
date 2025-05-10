namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create.Normal.Data;

using CustomCADs.UnitTests.Customs.Domain.Customs.Create.Normal;
using static CustomsData;

public class CustomCreateInvalidDescriptionData : CustomCreateData
{
    public CustomCreateInvalidDescriptionData()
    {
        Add(ValidName1, InvalidDescription1, true);
        Add(ValidName2, InvalidDescription2, false);
    }
}
