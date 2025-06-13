namespace CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Internal.Create.Data;

using static TagsData;

public class CreateTagInvalidData : TheoryData<string>
{
	public CreateTagInvalidData()
	{
		Add(InvalidName);
	}
}
