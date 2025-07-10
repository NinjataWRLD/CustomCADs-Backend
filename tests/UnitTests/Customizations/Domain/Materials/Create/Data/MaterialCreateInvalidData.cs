namespace CustomCADs.UnitTests.Customizations.Domain.Materials.Create.Data;

using static MaterialsData;

public class MaterialCreateInvalidData : TheoryData<string, decimal, decimal>
{
	public MaterialCreateInvalidData()
	{
		// Name
		Add(InvalidName, MaxValidDensity, MaxValidCost);
		Add(MinInvalidName, MinValidDensity, MinValidCost);
		Add(MaxInvalidName, MaxValidDensity, MaxValidCost);

		// Density
		Add(MinValidName, MinInvalidDensity, MinValidCost);
		Add(MaxValidName, MaxInvalidDensity, MaxValidCost);

		// Cost
		Add(MinValidName, MinValidDensity, MinInvalidCost);
		Add(MaxValidName, MaxValidDensity, MaxInvalidCost);
	}
}
