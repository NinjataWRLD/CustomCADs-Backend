namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create.Normal.Data;

using static ProductsData;

public class ProductCreateInvalidPriceData : ProductCreateData
{
	public ProductCreateInvalidPriceData()
	{
		Add(ValidName1, ValidDescription1, InvalidPrice1);
		Add(ValidName2, ValidDescription2, InvalidPrice2);
	}
}
