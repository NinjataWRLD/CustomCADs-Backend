namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create.Data;

using static ProductsData;

public class ProductCreateInvalidPriceData : ProductCreateData
{
	public ProductCreateInvalidPriceData()
	{
		Add(MinValidName, MinValidDescription, MinInvalidPrice);
		Add(MaxValidName, MaxValidDescription, MaxInvalidPrice);
	}
}
