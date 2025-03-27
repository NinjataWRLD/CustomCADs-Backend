namespace CustomCADs.UnitTests.Files.Application.Images.Commands.Shared.Create.Data;

using static ImagesData;

public class CreateImageInvalidKeyData : CreateImageData
{
    public CreateImageInvalidKeyData()
    {
        Add(InvalidKey, ValidContentType1);
        Add(InvalidKey, ValidContentType2);
    }
}
