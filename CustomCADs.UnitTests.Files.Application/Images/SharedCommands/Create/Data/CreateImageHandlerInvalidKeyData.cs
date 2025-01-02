namespace CustomCADs.UnitTests.Files.Application.Images.SharedCommands.Create.Data;

using static ImagesData;

public class CreateImageHandlerInvalidKeyData : CreateImageHandlerData
{
    public CreateImageHandlerInvalidKeyData()
    {
        Add(InvalidKey, ValidContentType1);
        Add(InvalidKey, ValidContentType2);
    }
}
