namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Edit.Data;

using static ProductsData;

public class EditProductInvalidNameData : EditProductData
{
    public EditProductInvalidNameData()
    {
        Add(InvalidName1, ValidDescription1, ValidPrice1);
        Add(InvalidName2, ValidDescription2, ValidPrice2);
    }
}
