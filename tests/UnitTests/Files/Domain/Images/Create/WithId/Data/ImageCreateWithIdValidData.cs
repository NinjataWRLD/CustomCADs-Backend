namespace CustomCADs.UnitTests.Files.Domain.Images.Create.WithId.Data;

using static ImagesData;

public class ImageCreateWithIdValidData : ImageCreateWithIdData
{
    public ImageCreateWithIdValidData()
    {
        Add(ValidKey1, ValidContentType1);
        Add(ValidKey2, ValidContentType2);
    }
}
