namespace CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.Create.Data;

using static CadsData;

public class CreateCadHandlerInvalidContentTypeData : CreateCadHandlerData
{
    public CreateCadHandlerInvalidContentTypeData()
    {
        Add(ValidKey1, InvalidContentType);
        Add(ValidKey2, InvalidContentType);
    }
}
