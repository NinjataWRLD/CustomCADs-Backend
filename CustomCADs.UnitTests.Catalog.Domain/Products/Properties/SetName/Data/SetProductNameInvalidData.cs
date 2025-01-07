namespace CustomCADs.UnitTests.Catalog.Domain.Products.Properties.SetName.Data;

using static ProductsData;

public class SetProductNameInvalidData : SetProductNameData
{
    public SetProductNameInvalidData()
    {
        Add(InvalidName1);
        Add(InvalidName2);
    }
}
