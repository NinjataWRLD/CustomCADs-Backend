namespace CustomCADs.UnitTests.Printing.Domain.Materials.Behaviors.Density.Data;

using static MaterialsData;

public class MaterialSetDensityInvalidData : TheoryData<decimal>
{
	public MaterialSetDensityInvalidData()
	{
		Add(MinInvalidDensity);
		Add(MaxInvalidDensity);
	}
}
