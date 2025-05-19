namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetPrice.Data;

using static ProductsData;

public class SetProductPriceValidData : SetProductPriceData
{
	public SetProductPriceValidData()
	{
		Add(ValidPrice1);
		Add(ValidPrice2);
	}
}
