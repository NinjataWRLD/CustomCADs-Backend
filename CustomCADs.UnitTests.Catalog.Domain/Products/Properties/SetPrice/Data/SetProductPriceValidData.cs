namespace CustomCADs.UnitTests.Catalog.Domain.Products.Properties.SetPrice.Data;

using static ProductsData;

public class SetProductPriceValidData : SetProductPriceData
{
    public SetProductPriceValidData()
    {
        Add(ValidPrice1);
        Add(ValidPrice2);
    }
}
