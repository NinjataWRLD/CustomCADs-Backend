namespace CustomCADs.UnitTests.Customizations.Domain.Customizations.Behaviors.Infill.Data;

using static CustomizationsData;

public class CustomizationSetInfillInvalidData : TheoryData<decimal>
{
	public CustomizationSetInfillInvalidData()
	{
		Add(MinInvalidInfill);
		Add(MaxInvalidInfill);
	}
}
