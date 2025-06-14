namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Creator.Edit.Data;

using static ProductsData;

public class EditProductInvalidPriceData : EditProductData
{
	public EditProductInvalidPriceData()
	{
		Add(MinValidName, MinValidDescription, MinInvalidPrice);
		Add(MaxValidName, MaxValidDescription, MaxInvalidPrice);
	}
}
