namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Creator.Edit.Data;

using CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Creator.Edit;
using static ProductsData;

public class EditProductInvalidDescriptionData : EditProductData
{
    public EditProductInvalidDescriptionData()
    {
        Add(ValidName1, InvalidDescription1, ValidPrice1);
        Add(ValidName2, InvalidDescription2, ValidPrice2);
    }
}
