namespace CustomCADs.UnitTests.Files.Application.Images.SharedCommands.SetContentType.Data;

using static ImagesData;

public class SetImageContentTypeHandlerInvalidData : SetImageContentTypeHandlerData
{
    public SetImageContentTypeHandlerInvalidData()
    {
        Add(InvalidContentType);
    }
}
