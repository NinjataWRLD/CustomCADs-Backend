namespace CustomCADs.UnitTests.Files.Domain.Images.Create.Data;

using static ImagesData;

public class ImageCreateInvalidData : TheoryData<string, string>
{
	public ImageCreateInvalidData()
	{
		// Key
		Add(InvalidKey, ValidContentType);

		// Content Type
		Add(ValidKey, InvalidContentType);
	}
}
