namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create.WithId.Data;

using static CustomsData;

public class CustomCreateWitIdValidData : CustomCreateWithIdData
{
	public CustomCreateWitIdValidData()
	{
		Add(MinValidName, MinValidDescription, true);
		Add(MaxValidName, MaxValidDescription, false);
	}
}
