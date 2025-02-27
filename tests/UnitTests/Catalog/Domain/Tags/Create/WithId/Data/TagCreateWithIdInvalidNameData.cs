namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Create.WithId.Data;

using static TagsData;

public class TagCreateWithIdInvalidNameData : TagCreateWithIdData
{
    public TagCreateWithIdInvalidNameData()
    {
        Add(ValidId, InvalidName1);
        Add(ValidId, InvalidName2);
    }
}
