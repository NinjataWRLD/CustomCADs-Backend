namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Creator.Edit.Data;

using static ProductsData;

public class EditProductInvalidDescriptionData : EditProductData
{
	public EditProductInvalidDescriptionData()
	{
		Add(MinValidName, MinInvalidDescription, MinValidPrice);
		Add(MaxValidName, MaxInvalidDescription, MaxValidPrice);
	}
}
