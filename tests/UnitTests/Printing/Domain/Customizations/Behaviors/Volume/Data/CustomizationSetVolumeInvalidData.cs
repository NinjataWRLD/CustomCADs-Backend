namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Behaviors.Volume.Data;

using static CustomizationsData;

public class CustomizationSetVolumeInvalidData : TheoryData<decimal>
{
	public CustomizationSetVolumeInvalidData()
	{
		Add(MinInvalidVolume);
		Add(MaxInvalidVolume);
	}
}
