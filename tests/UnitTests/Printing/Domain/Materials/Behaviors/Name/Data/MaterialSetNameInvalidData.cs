namespace CustomCADs.UnitTests.Printing.Domain.Materials.Behaviors.Name.Data;

using static MaterialsData;

public class MaterialSetNameInvalidData : TheoryData<string>
{
	public MaterialSetNameInvalidData()
	{
		Add(InvalidName);
		Add(MinInvalidName);
		Add(MaxInvalidName);
	}
}
