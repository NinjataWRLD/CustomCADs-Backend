namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Creator.Edit.Data;

using static ProductsData;

public class EditProductValidData : EditProductData
{
	public EditProductValidData()
	{
		Add(MinValidName, MinValidDescription, MinValidPrice);
		Add(MaxValidName, MaxValidDescription, MaxValidPrice);
	}
}
