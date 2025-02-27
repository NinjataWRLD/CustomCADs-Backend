namespace CustomCADs.UnitTests.Files.Domain.Images.Create.WithId.Data;

using static ImagesData;

public class ImageCreateValidData : ImageCreateWithIdData
{
    public ImageCreateValidData()
    {
        Add(ValidKey1, ValidContentType1);
        Add(ValidKey2, ValidContentType2);
    }
}
