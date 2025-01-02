namespace CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.Create.Data;

using static CadsData;

public class CreateCadHandlerInvalidKeyData : CreateCadHandlerData
{
    public CreateCadHandlerInvalidKeyData()
    {
        Add(InvalidKey, ValidContentType1);
        Add(InvalidKey, ValidContentType2);
    }
}
