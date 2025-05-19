namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Create.WithId.Data;

using static TagsData;

public class TagCreateWithIdValidData : TagCreateWithIdData
{
	public TagCreateWithIdValidData()
	{
		Add(ValidId, ValidName1);
		Add(ValidId, ValidName2);
	}
}
