namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create.WithId.Data;

using static ProductsData;

public class ProductCreateWithIdInvalidPriceData : ProductCreateWithIdData
{
	public ProductCreateWithIdInvalidPriceData()
	{
		Add(MinValidName, MinValidDescription, MinInvalidPrice);
		Add(MaxValidName, MaxValidDescription, MaxInvalidPrice);
	}
}
