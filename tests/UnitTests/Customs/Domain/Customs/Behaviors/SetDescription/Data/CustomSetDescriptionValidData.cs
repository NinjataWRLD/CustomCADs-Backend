namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetDescription.Data;

using static CustomsData;

public class CustomSetDescriptionValidData : CustomSetDescriptionData
{
	public CustomSetDescriptionValidData()
	{
		Add(MinValidDescription);
		Add(MaxValidDescription);
	}
}
