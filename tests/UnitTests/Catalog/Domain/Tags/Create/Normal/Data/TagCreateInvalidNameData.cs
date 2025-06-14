namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Create.Normal.Data;

using static TagsData;

public class TagCreateInvalidNameData : TagCreateData
{
	public TagCreateInvalidNameData()
	{
		Add(MinInvalidName);
		Add(MaxInvalidName);
	}
}
