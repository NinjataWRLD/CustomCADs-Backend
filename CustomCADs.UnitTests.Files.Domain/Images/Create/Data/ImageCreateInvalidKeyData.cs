namespace CustomCADs.UnitTests.Files.Domain.Images.Create.Data;

public class ImageCreateInvalidKeyData : ImageCreateData
{
    public ImageCreateInvalidKeyData()
    {
        Add(ImageInvalidKey, ImageValidContentType1);
        Add(ImageInvalidKey, ImageValidContentType2);
    }
}
