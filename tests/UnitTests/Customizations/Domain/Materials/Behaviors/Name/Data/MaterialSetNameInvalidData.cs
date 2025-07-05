namespace CustomCADs.UnitTests.Customizations.Domain.Materials.Behaviors.Name.Data;

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
