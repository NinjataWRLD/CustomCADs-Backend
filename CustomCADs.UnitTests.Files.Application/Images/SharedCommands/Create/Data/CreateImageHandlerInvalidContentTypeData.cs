namespace CustomCADs.UnitTests.Files.Application.Images.SharedCommands.Create.Data;

using static ImagesData;

public class CreateImageHandlerInvalidContentTypeData : CreateImageHandlerData
{
    public CreateImageHandlerInvalidContentTypeData()
    {
        Add(ValidKey1, InvalidContentType);
        Add(ValidKey2, InvalidContentType);
    }
}
