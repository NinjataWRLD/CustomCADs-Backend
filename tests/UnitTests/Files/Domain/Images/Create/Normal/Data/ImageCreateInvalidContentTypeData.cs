namespace CustomCADs.UnitTests.Files.Domain.Images.Create.Normal.Data;

using static ImagesData;

public class ImageCreateInvalidContentTypeData : ImageCreateData
{
    public ImageCreateInvalidContentTypeData()
    {
        Add(ValidKey1, InvalidContentType);
        Add(ValidKey2, InvalidContentType);
    }
}
