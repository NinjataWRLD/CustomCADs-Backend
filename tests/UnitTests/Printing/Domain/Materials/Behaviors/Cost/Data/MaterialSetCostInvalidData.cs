namespace CustomCADs.UnitTests.Printing.Domain.Materials.Behaviors.Cost.Data;

using static MaterialsData;

public class MaterialSetCostInvalidData : TheoryData<decimal>
{
	public MaterialSetCostInvalidData()
	{
		Add(MinInvalidCost);
		Add(MaxInvalidCost);
	}
}
