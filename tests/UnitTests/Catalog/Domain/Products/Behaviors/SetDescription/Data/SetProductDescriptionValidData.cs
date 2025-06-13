namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetDescription.Data;

using static ProductsData;

public class SetProductDescriptionValidData : SetProductDescriptionData
{
	public SetProductDescriptionValidData()
	{
		Add(MinValidDescription);
		Add(MaxValidDescription);
	}
}
