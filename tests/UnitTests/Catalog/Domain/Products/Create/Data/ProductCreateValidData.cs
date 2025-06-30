namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create.Data;

using static ProductsData;

public class ProductCreateValidData : ProductCreateData
{
	public ProductCreateValidData()
	{
		Add(MinValidName, MinValidDescription, MinValidPrice);
		Add(MaxValidName, MaxValidDescription, MaxValidPrice);
	}
}
