namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create.Normal.Data;

using static ProductsData;

public class ProductCreateInvalidDescriptionData : ProductCreateData
{
	public ProductCreateInvalidDescriptionData()
	{
		Add(ValidName1, InvalidDescription1, ValidPrice1);
		Add(ValidName2, InvalidDescription2, ValidPrice2);
	}
}
