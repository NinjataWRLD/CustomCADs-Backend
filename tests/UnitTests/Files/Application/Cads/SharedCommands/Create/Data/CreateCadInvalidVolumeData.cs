namespace CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.Create.Data;

using static CadsData;

public class CreateCadInvalidVolumeData : CreateCadData
{
    public CreateCadInvalidVolumeData()
    {
        Add(ValidKey1, ValidContentType1, InvalidVolume);
        Add(ValidKey2, ValidContentType2, InvalidVolume);
    }
}
