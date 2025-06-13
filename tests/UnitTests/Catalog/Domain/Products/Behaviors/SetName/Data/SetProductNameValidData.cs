namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetName.Data;

using static ProductsData;

public class SetProductNameValidData : SetProductNameData
{
	public SetProductNameValidData()
	{
		Add(MinValidName);
		Add(MaxValidName);
	}
}
