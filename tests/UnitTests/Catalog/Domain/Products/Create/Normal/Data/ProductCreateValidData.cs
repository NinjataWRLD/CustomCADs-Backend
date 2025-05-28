namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create.Normal.Data;

using CustomCADs.UnitTests.Catalog.Domain.Products.Create.Normal;
using static ProductsData;

public class ProductCreateValidData : ProductCreateData
{
	public ProductCreateValidData()
	{
		Add(ValidName1, ValidDescription1, ValidPrice1);
		Add(ValidName2, ValidDescription2, ValidPrice2);
	}
}
