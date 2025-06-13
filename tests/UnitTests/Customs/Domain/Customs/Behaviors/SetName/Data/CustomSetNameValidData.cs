namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetName.Data;

using static CustomsData;

public class CustomSetNameValidData : CustomSetNameData
{
	public CustomSetNameValidData()
	{
		Add(MinValidName);
		Add(MaxValidName);
	}
}
