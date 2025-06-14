namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetPrice.Data;

using static ProductsData;

public class SetProductPriceInvalidData : SetProductPriceData
{
	public SetProductPriceInvalidData()
	{
		Add(MinInvalidPrice);
		Add(MaxInvalidPrice);
	}
}
