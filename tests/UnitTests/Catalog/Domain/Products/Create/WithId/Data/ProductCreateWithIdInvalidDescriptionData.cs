namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create.WithId.Data;

using static ProductsData;

public class ProductCreateWithIdInvalidDescriptionData : ProductCreateWithIdData
{
    public ProductCreateWithIdInvalidDescriptionData()
    {
        Add(ValidName1, InvalidDescription1, ValidPrice1);
        Add(ValidName2, InvalidDescription2, ValidPrice2);
    }
}
