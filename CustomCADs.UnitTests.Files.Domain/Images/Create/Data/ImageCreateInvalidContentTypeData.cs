namespace CustomCADs.UnitTests.Files.Domain.Images.Create.Data;

public class ImageCreateInvalidContentTypeData : ImageCreateData
{
    public ImageCreateInvalidContentTypeData()
    {
        Add(ImageValidKey1, ImageInvalidContentType);
        Add(ImageValidKey2, ImageInvalidContentType);
    }
}
