namespace CustomCADs.UnitTests.Files.Domain.Images.Create.Data;

using static ImagesData;

public class ImageCreateValidData : ImageCreateData
{
    public ImageCreateValidData()
    {
        Add(ValidKey1, ValidContentType1);
        Add(ValidKey2, ValidContentType2);
    }
}
