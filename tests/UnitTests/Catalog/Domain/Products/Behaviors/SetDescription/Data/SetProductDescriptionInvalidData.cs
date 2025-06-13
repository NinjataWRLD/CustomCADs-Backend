namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetDescription.Data;

using static ProductsData;

public class SetProductDescriptionInvalidData : SetProductDescriptionData
{
	public SetProductDescriptionInvalidData()
	{
		Add(MinInvalidDescription);
		Add(MaxInvalidDescription);
	}
}
