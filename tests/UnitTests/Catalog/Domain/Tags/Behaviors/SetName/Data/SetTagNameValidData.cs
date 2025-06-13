namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Behaviors.SetName.Data;

using static TagsData;

public class SetTagNameValidData : SetTagNameData
{
	public SetTagNameValidData()
	{
		Add(MinValidName);
		Add(MaxValidName);
	}
}
