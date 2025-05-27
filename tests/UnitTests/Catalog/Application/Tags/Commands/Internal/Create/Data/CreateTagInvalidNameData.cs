namespace CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Internal.Create.Data;

using CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Internal.Create;
using static TagsData;

public class CreateTagInvalidNameData : CreateTagData
{
	public CreateTagInvalidNameData()
	{
		Add(InvalidName1);
		Add(InvalidName2);
		Add(InvalidName3);
	}
}
