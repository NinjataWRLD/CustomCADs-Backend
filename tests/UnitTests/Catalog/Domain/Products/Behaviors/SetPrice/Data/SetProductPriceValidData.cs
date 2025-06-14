namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetPrice.Data;

using static ProductsData;

public class SetProductPriceValidData : SetProductPriceData
{
	public SetProductPriceValidData()
	{
		Add(MinValidPrice);
		Add(MaxValidPrice);
	}
}
