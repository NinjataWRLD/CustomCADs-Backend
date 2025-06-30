namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create.Data;

using static CustomsData;

public class CustomCreateInvalidDescriptionData : CustomCreateData
{
	public CustomCreateInvalidDescriptionData()
	{
		Add(MinValidName, MinInvalidDescription, true);
		Add(MaxValidName, MaxInvalidDescription, false);
	}
}
