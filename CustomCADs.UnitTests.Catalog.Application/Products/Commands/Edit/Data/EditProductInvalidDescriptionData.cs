namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Edit.Data;

using static ProductsData;

public class EditProductInvalidDescriptionData : EditProductData
{
    public EditProductInvalidDescriptionData()
    {
        Add(ValidName1, InvalidDescription1, ValidPrice1);
        Add(ValidName2, InvalidDescription2, ValidPrice2);
    }
}
