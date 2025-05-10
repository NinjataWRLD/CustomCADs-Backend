namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Edit.Data;

using static CustomsData;

public class EditCustomInvalidNameData : EditCustomData
{
    public EditCustomInvalidNameData()
    {
        Add(InvalidName1, ValidDescription1);
        Add(InvalidName2, ValidDescription2);
    }
}
