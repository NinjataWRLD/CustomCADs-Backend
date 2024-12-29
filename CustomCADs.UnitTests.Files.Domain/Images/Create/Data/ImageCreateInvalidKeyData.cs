namespace CustomCADs.UnitTests.Files.Domain.Images.Create.Data;

using static ImagesData;

public class ImageCreateInvalidKeyData : ImageCreateData
{
    public ImageCreateInvalidKeyData()
    {
        Add(InvalidKey, ValidContentType1);
        Add(InvalidKey, ValidContentType2);
    }
}
