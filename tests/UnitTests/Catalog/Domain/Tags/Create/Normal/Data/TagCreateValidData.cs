namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Create.Normal.Data;

using static TagsData;

public class TagCreateValidData : TagCreateData
{
	public TagCreateValidData()
	{
		Add(MinValidName);
		Add(MaxValidName);
	}
}
