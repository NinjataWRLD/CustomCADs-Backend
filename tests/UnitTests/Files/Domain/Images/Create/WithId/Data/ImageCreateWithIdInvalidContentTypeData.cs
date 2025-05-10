namespace CustomCADs.UnitTests.Files.Domain.Images.Create.WithId.Data;

using static ImagesData;

public class ImageCreateWithIdInvalidContentTypeData : ImageCreateWithIdData
{
    public ImageCreateWithIdInvalidContentTypeData()
    {
        Add(ValidKey1, InvalidContentType);
        Add(ValidKey2, InvalidContentType);
    }
}
