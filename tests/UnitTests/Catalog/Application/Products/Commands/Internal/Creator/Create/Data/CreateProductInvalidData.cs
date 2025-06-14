namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Creator.Create.Data;

using static ProductsData;

public class CreateProductInvalidData : TheoryData<string, string, decimal>
{
	public CreateProductInvalidData()
	{
		// Name
		Add(MinInvalidName, MinValidDescription, MinValidPrice);
		Add(MaxInvalidName, MaxValidDescription, MaxValidPrice);

		// Descripion
		Add(MinValidName, MinInvalidDescription, MinValidPrice);
		Add(MaxValidName, MaxInvalidDescription, MaxValidPrice);

		// Price
		Add(MinValidName, MinValidDescription, MinInvalidPrice);
		Add(MaxValidName, MaxValidDescription, MaxInvalidPrice);
	}
}
