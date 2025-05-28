namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetDescription.Data;

using static ProductsData;

public class SetProductDescriptionValidData : SetProductDescriptionData
{
	public SetProductDescriptionValidData()
	{
		Add(ValidDescription1);
		Add(ValidDescription2);
	}
}
