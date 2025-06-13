namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create.Normal.Data;

using static ProductsData;

public class ProductCreateInvalidNameData : ProductCreateData
{
	public ProductCreateInvalidNameData()
	{
		Add(MinInvalidName, MinValidDescription, MinValidPrice);
		Add(MaxInvalidName, MaxValidDescription, MaxValidPrice);
	}
}
