namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Client.Create.Data;

using static CustomsData;

public class CreateCustomInvalidDescriptionData : CreateCustomData
{
    public CreateCustomInvalidDescriptionData()
    {
        Add(ValidName1, InvalidDescription1, false, ValidBuyerId1);
        Add(ValidName2, InvalidDescription2, true, ValidBuyerId2);
    }
}
