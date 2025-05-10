namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Creator.Edit.Data;

using static ProductsData;

public class EditProductInvalidPriceData : EditProductData
{
    public EditProductInvalidPriceData()
    {
        Add(ValidName1, ValidDescription1, InvalidPrice1);
        Add(ValidName2, ValidDescription2, InvalidPrice2);
    }
}
