namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Create.Normal.Data;

using static TagsData;

public class TagCreateValidData : TagCreateData
{
    public TagCreateValidData()
    {
        Add(ValidName1);
        Add(ValidName2);
    }
}
