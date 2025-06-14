namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Create.WithId.Data;

using static TagsData;

public class TagCreateWithIdValidData : TagCreateWithIdData
{
	public TagCreateWithIdValidData()
	{
		Add(MinValidName);
		Add(MaxValidName);
	}
}
