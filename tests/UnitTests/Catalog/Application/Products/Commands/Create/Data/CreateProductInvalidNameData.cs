namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Create.Data;

using static ProductsData;

public class CreateProductInvalidNameData : CreateProductData
{
    public CreateProductInvalidNameData()
    {
        Add(InvalidName1, ValidDescription1, ValidPrice1);
        Add(InvalidName2, ValidDescription2, ValidPrice2);
    }
}
