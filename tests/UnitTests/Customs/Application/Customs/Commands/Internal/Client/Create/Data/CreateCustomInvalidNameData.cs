namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Client.Create.Data;

using static CustomsData;

public class CreateCustomInvalidNameData : CreateCustomData
{
    public CreateCustomInvalidNameData()
    {
        Add(InvalidName1, ValidDescription1, false, ValidBuyerId1);
        Add(InvalidName2, ValidDescription2, true, ValidBuyerId2);
    }
}
