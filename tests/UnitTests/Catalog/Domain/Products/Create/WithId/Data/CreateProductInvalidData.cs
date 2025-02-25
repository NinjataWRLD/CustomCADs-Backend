namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create.WithId.Data;

using static ProductsData;

public class ProductCreateWithIdInvalidNameData : ProductCreateWithIdData
{
    public ProductCreateWithIdInvalidNameData()
    {
        Add(ValidId, InvalidName1, ValidDescription1, ValidPrice1);
        Add(ValidId, InvalidName2, ValidDescription2, ValidPrice2);
    }
}
