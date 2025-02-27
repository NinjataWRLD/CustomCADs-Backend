namespace CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Edit.Data;

using static TagsData;

public class EditTagValidData : EditTagData
{
    public EditTagValidData()
    {
        Add(ValidName1);
        Add(ValidName2);
    }
}
