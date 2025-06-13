namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create.WithId.Data;

using static ProductsData;

public class ProductCreateWithIdValidData : ProductCreateWithIdData
{
	public ProductCreateWithIdValidData()
	{
		Add(MinValidName, MinValidDescription, MinValidPrice);
		Add(MaxValidName, MaxValidDescription, MaxValidPrice);
	}
}
