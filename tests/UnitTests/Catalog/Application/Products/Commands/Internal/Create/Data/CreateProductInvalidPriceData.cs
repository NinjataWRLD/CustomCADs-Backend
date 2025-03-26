namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Create.Data;

using CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Create;
using static ProductsData;

public class CreateProductInvalidPriceData : CreateProductData
{
    public CreateProductInvalidPriceData()
    {
        Add(ValidName1, ValidDescription1, InvalidPrice1);
        Add(ValidName2, ValidDescription2, InvalidPrice2);
    }
}
