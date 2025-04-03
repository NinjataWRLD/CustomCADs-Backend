namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create.Normal.Data;

using CustomCADs.UnitTests.Customs.Domain.Customs.Create.Normal;
using static CustomsData;

public class CustomCreateInvalidNameData : CustomCreateData
{
    public CustomCreateInvalidNameData()
    {
        Add(InvalidName1, ValidDescription1, true, ValidBuyerId1);
        Add(InvalidName2, ValidDescription2, false, ValidBuyerId2);
    }
}
