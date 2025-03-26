namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Create.Data;

using CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Create;
using static ProductsData;

public class CreateProductValidData : CreateProductData
{
    public CreateProductValidData()
    {
        Add(ValidName1, ValidDescription1, ValidPrice1);
        Add(ValidName2, ValidDescription2, ValidPrice2);
    }
}
