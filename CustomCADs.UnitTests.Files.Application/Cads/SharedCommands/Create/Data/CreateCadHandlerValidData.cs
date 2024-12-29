namespace CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.Create.Data;

using static ImagesData;

public class CreateCadHandlerValidData : CreateCadHandlerData
{
    public CreateCadHandlerValidData()
    {
        Add(ValidKey1, ValidContentType1);
        Add(ValidKey2, ValidContentType2);
    }
}
