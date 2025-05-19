namespace CustomCADs.UnitTests.Files.Domain.Images.Create.WithId.Data;

using static ImagesData;

public class ImageCreateInvalidKeyData : ImageCreateWithIdData
{
	public ImageCreateInvalidKeyData()
	{
		Add(InvalidKey, ValidContentType1);
		Add(InvalidKey, ValidContentType2);
	}
}
