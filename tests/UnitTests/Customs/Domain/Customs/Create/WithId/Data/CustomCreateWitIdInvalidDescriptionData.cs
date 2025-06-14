namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create.WithId.Data;

using static CustomsData;

public class CustomCreateWitIdInvalidDescriptionData : CustomCreateWithIdData
{
	public CustomCreateWitIdInvalidDescriptionData()
	{
		Add(MinValidName, MinInvalidDescription, true);
		Add(MaxValidName, MaxInvalidDescription, false);
	}
}
