namespace CustomCADs.UnitTests.Files.Application.Images.Commands.Shared.Create.Data;

using static ImagesData;

public class CreateImageInvalidData : TheoryData<string, string>
{
	public CreateImageInvalidData()
	{
		// Key
		Add(InvalidKey, ValidContentType);

		// Content Type
		Add(ValidKey, InvalidContentType);
	}
}
