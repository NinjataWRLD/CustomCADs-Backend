namespace CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Internal.Edit.Data;

using static TagsData;

public class EditTagInvalidData : TheoryData<string>
{
	public EditTagInvalidData()
	{
		Add(InvalidName);
	}
}
