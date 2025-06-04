namespace CustomCADs.UnitTests.Files.Domain.Images.Create.WithId.Data;

using static ImagesData;

public class ImageCreateWithIdInvalidKeyData : ImageCreateWithIdData
{
	public ImageCreateWithIdInvalidKeyData()
	{
		Add(InvalidKey, ValidContentType1);
		Add(InvalidKey, ValidContentType2);
	}
}
