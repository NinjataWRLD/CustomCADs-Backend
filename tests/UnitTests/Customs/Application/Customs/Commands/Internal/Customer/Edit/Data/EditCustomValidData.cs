namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Edit.Data;

using CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Edit;
using static CustomsData;

public class EditCustomValidData : EditCustomData
{
	public EditCustomValidData()
	{
		Add(ValidName1, ValidDescription1);
		Add(ValidName2, ValidDescription2);
	}
}
