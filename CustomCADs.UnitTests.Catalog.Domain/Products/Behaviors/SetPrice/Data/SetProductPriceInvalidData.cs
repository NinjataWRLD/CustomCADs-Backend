namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetPrice.Data;

using static ProductsData;

public class SetProductPriceInvalidData : SetProductPriceData
{
    public SetProductPriceInvalidData()
    {
        Add(InvalidPrice1);
        Add(InvalidPrice2);
    }
}
