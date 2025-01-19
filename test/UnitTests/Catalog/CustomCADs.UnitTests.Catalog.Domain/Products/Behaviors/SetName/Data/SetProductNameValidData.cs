namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetName.Data;

using static ProductsData;

public class SetProductNameValidData : SetProductNameData
{
    public SetProductNameValidData()
    {
        Add(ValidName1);
        Add(ValidName2);
    }
}
