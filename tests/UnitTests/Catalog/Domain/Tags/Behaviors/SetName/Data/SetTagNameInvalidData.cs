namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Behaviors.SetName.Data;

using static TagsData;

public class SetTagNameInvalidData : SetTagNameData
{
	public SetTagNameInvalidData()
	{
		Add(MinInvalidName);
		Add(MaxInvalidName);
	}
}
