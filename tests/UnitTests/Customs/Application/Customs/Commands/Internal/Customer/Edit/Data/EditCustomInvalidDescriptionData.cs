namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Edit.Data;

using CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Edit;
using static CustomsData;

public class EditCustomInvalidDescriptionData : EditCustomData
{
    public EditCustomInvalidDescriptionData()
    {
        Add(ValidName1, InvalidDescription1);
        Add(ValidName2, InvalidDescription2);
    }
}
