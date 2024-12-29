namespace CustomCADs.UnitTests.Files.Domain.Images.Create.Data;

public class ImageCreateValidData : ImageCreateData
{
    public ImageCreateValidData()
    {
        Add(ImageValidKey1, ImageValidContentType1);
        Add(ImageValidKey2, ImageValidContentType2);
    }
}
