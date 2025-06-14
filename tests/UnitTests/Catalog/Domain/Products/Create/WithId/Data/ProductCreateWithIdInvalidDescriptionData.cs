namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create.WithId.Data;

using static ProductsData;

public class ProductCreateWithIdInvalidDescriptionData : ProductCreateWithIdData
{
	public ProductCreateWithIdInvalidDescriptionData()
	{
		Add(MinValidName, MinInvalidDescription, MinValidPrice);
		Add(MaxValidName, MaxInvalidDescription, MaxValidPrice);
	}
}
