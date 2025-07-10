namespace CustomCADs.UnitTests.Customizations.Domain.Customizations.Behaviors.Scale.Data;

using static CustomizationsData;

public class CustomizationSetScaleInvalidData : TheoryData<decimal>
{
	public CustomizationSetScaleInvalidData()
	{
		Add(MinInvalidScale);
		Add(MaxInvalidScale);
	}
}
