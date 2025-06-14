namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Creator.Edit.Data;

using static ProductsData;

public class EditProductInvalidNameData : EditProductData
{
	public EditProductInvalidNameData()
	{
		Add(MinInvalidName, MinValidDescription, MinValidPrice);
		Add(MaxInvalidName, MaxValidDescription, MaxValidPrice);
	}
}
