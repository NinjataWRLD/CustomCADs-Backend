namespace CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.Create.Data;

using static CadsData;

public class CreateCadInvalidContentTypeData : CreateCadData
{
    public CreateCadInvalidContentTypeData()
    {
        Add(ValidKey1, InvalidContentType, ValidVolume1);
        Add(ValidKey2, InvalidContentType, ValidVolume2);
    }
}
