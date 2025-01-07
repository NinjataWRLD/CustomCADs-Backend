namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create.Data;

using static ProductsData;

public class CreateProductInvalidDescriptionData : CreateProductData
{
    public CreateProductInvalidDescriptionData()
    {
        Add(ValidName1, InvalidDescription1, ValidPrice1);
        Add(ValidName2, InvalidDescription2, ValidPrice2);
    }
}
