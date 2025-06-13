namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetName.Data;

using static CustomsData;

public class CustomSetNameInvalidData : CustomSetNameData
{
	public CustomSetNameInvalidData()
	{
		Add(MinInvalidName);
		Add(MaxInvalidName);
	}
}
