namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create.Normal.Data;

using static ProductsData;

public class ProductCreateInvalidNameData : ProductCreateData
{
	public ProductCreateInvalidNameData()
	{
		Add(InvalidName1, ValidDescription1, ValidPrice1);
		Add(InvalidName2, ValidDescription2, ValidPrice2);
	}
}
