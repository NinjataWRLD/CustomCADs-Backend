namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create.Data;

using static ProductsData;

public class CreateProductValidData : CreateProductData
{
    public CreateProductValidData()
    {
        Add(ValidName1, ValidDescription1, ValidPrice1);
        Add(ValidName2, ValidDescription2, ValidPrice2);
    }
}
