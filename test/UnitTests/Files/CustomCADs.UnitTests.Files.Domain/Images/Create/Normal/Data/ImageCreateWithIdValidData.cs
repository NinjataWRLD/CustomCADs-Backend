namespace CustomCADs.UnitTests.Files.Domain.Images.Create.Normal.Data;

using CustomCADs.UnitTests.Files.Domain.Images.Create.Normal;
using static ImagesData;

public class ImageCreateWithIdValidData : ImageCreateData
{
    public ImageCreateWithIdValidData()
    {
        Add(ValidKey1, ValidContentType1);
        Add(ValidKey2, ValidContentType2);
    }
}
