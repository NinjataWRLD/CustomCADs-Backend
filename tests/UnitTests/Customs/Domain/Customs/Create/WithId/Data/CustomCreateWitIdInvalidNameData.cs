namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create.WithId.Data;

using static CustomsData;

public class CustomCreateWitIdInvalidNameData : CustomCreateWithIdData
{
	public CustomCreateWitIdInvalidNameData()
	{
		Add(MinInvalidName, MinValidDescription, true);
		Add(MaxInvalidName, MaxValidDescription, false);
	}
}
