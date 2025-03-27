namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Create.Data;

using CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Create;
using static ProductsData;

public class CreateProductInvalidDescriptionData : CreateProductData
{
    public CreateProductInvalidDescriptionData()
    {
        Add(ValidName1, InvalidDescription1, ValidPrice1);
        Add(ValidName2, InvalidDescription2, ValidPrice2);
    }
}
