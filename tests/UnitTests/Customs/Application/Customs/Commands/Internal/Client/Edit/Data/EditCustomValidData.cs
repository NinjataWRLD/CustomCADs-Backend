namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Client.Edit.Data;

using static CustomsData;

public class EditCustomValidData : EditCustomData
{
    public EditCustomValidData()
    {
        Add(ValidName1, ValidDescription1);
        Add(ValidName2, ValidDescription2);
    }
}
