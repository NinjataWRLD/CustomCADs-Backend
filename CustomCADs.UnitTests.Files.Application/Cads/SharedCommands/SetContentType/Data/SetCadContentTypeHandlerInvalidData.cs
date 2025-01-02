namespace CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.SetContentType.Data;

using static CadsData;

public class SetCadContentTypeHandlerInvalidData : SetCadContentTypeHandlerData
{
    public SetCadContentTypeHandlerInvalidData()
    {
        Add(InvalidContentType);
    }
}
