namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create.Data;

using static ProductsData;

public class ProductCreateInvalidDescriptionData : ProductCreateData
{
	public ProductCreateInvalidDescriptionData()
	{
		Add(MinValidName, MinInvalidDescription, MinValidPrice);
		Add(MaxValidName, MaxInvalidDescription, MaxValidPrice);
	}
}
