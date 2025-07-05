namespace CustomCADs.UnitTests.Customizations.Domain.Customizations.Behaviors.Color.Data;

using static CustomizationsData;

public class CustomizationSetColorInvalidData : TheoryData<string>
{
	public CustomizationSetColorInvalidData()
	{
		Add(InvalidColor);
		Add(MinInvalidColor);
		Add(MaxInvalidColor);
	}
}
