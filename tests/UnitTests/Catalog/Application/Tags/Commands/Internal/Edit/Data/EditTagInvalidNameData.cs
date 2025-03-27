namespace CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Internal.Edit.Data;

using CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Internal.Edit;
using static TagsData;

public class EditTagInvalidNameData : EditTagData
{
    public EditTagInvalidNameData()
    {
        Add(InvalidName1);
        Add(InvalidName2);
        Add(InvalidName3);
    }
}
