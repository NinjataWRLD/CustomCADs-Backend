namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create.WithId.Data;

using static ProductsData;

public class ProductCreateWithIdInvalidDescriptionData : ProductCreateWithIdData
{
    public ProductCreateWithIdInvalidDescriptionData()
    {
        Add(ValidId, ValidName1, InvalidDescription1, ValidPrice1);
        Add(ValidId, ValidName2, InvalidDescription2, ValidPrice2);
    }
}
