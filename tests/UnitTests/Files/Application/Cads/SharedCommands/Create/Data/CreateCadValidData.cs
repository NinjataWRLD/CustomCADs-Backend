namespace CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.Create.Data;

using static CadsData;

public class CreateCadValidData : CreateCadData
{
    public CreateCadValidData()
    {
        Add(ValidKey1, ValidContentType1, ValidVolume1);
        Add(ValidKey2, ValidContentType2, ValidVolume2);
    }
}
