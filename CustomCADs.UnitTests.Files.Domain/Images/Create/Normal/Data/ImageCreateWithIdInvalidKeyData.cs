namespace CustomCADs.UnitTests.Files.Domain.Images.Create.Normal.Data;

using CustomCADs.UnitTests.Files.Domain.Images.Create.Normal;
using static ImagesData;

public class ImageCreateWithIdInvalidKeyData : ImageCreateData
{
    public ImageCreateWithIdInvalidKeyData()
    {
        Add(InvalidKey, ValidContentType1);
        Add(InvalidKey, ValidContentType2);
    }
}
