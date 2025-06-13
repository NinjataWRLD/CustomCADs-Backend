namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create.Normal.Data;

using static CustomsData;

public class CustomCreateValidData : CustomCreateData
{
	public CustomCreateValidData()
	{
		Add(MinValidName, MinValidDescription, true);
		Add(MaxValidName, MaxValidDescription, false);
	}
}
