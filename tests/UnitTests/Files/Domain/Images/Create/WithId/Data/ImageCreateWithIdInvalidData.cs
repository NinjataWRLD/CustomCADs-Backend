namespace CustomCADs.UnitTests.Files.Domain.Images.Create.WithId.Data;

using static ImagesData;

public class ImageCreateWithIdInvalidData : TheoryData<string, string>
{
	public ImageCreateWithIdInvalidData()
	{
		// Key
		Add(InvalidKey, ValidContentType);

		// Content Type
		Add(ValidKey, InvalidContentType);
	}
}
