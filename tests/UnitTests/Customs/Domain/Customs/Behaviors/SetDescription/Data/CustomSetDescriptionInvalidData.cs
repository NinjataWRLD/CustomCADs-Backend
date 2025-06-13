namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetDescription.Data;

using static CustomsData;

public class CustomSetDescriptionInvalidData : CustomSetDescriptionData
{
	public CustomSetDescriptionInvalidData()
	{
		Add(MinInvalidDescription);
		Add(MaxInvalidDescription);
	}
}
