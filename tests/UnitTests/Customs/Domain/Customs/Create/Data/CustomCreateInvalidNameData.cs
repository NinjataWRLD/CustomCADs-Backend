namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create.Data;

using static CustomsData;

public class CustomCreateInvalidNameData : CustomCreateData
{
	public CustomCreateInvalidNameData()
	{
		Add(MinInvalidName, MinValidDescription, true);
		Add(MaxInvalidName, MaxValidDescription, false);
	}
}
