namespace CustomCADs.UnitTests.Files.Domain.Images.Create.Normal.Data;

using CustomCADs.UnitTests.Files.Domain.Images.Create.Normal;
using static ImagesData;

public class ImageCreateWithIdInvalidContentTypeData : ImageCreateData
{
	public ImageCreateWithIdInvalidContentTypeData()
	{
		Add(ValidKey1, InvalidContentType);
		Add(ValidKey2, InvalidContentType);
	}
}
