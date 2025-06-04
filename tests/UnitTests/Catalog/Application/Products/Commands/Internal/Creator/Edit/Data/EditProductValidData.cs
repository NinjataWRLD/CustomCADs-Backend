namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Creator.Edit.Data;

using static ProductsData;

public class EditProductValidData : EditProductData
{
	public EditProductValidData()
	{
		Add(ValidName1, ValidDescription1, ValidPrice1);
		Add(ValidName2, ValidDescription2, ValidPrice2);
	}
}
