namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create.WithId.Data;

using static ProductsData;

public class ProductCreateWithIdInvalidPriceData : ProductCreateWithIdData
{
    public ProductCreateWithIdInvalidPriceData()
    {
        Add(ValidId, ValidName1, ValidDescription1, InvalidPrice1);
        Add(ValidId, ValidName2, ValidDescription2, InvalidPrice2);
    }
}
