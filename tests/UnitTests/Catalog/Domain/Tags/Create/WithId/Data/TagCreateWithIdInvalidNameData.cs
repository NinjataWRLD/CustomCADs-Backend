namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Create.WithId.Data;

using static TagsData;

public class TagCreateWithIdInvalidNameData : TagCreateWithIdData
{
	public TagCreateWithIdInvalidNameData()
	{
		Add(InvalidName1);
		Add(InvalidName2);
	}
}
