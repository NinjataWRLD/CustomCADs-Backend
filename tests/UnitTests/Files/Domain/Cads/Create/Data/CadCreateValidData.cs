namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.Data;

using CustomCADs.UnitTests.Files.Domain.Cads.Create;
using static CadsData;

public class CadCreateValidData : CadCreateData
{
    public CadCreateValidData()
    {
        Add(ValidKey1, ValidContentType1, ValidVolume1, ValidCoord1, ValidCoord1, ValidCoord1);
        Add(ValidKey2, ValidContentType2, ValidVolume2, ValidCoord2, ValidCoord2, ValidCoord2);
    }
}
