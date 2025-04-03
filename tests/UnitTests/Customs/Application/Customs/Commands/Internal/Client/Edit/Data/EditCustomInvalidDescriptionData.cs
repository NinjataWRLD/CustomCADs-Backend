namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Client.Edit.Data;

using static CustomsData;

public class EditCustomInvalidDescriptionData : EditCustomData
{
    public EditCustomInvalidDescriptionData()
    {
        Add(ValidName1, InvalidDescription1);
        Add(ValidName2, InvalidDescription2);
    }
}
