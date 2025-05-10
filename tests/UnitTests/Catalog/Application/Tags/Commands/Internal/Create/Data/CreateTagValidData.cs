namespace CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Internal.Create.Data;

using static TagsData;

public class CreateTagValidData : CreateTagData
{
    public CreateTagValidData()
    {
        Add(ValidName1);
        Add(ValidName2);
    }
}
