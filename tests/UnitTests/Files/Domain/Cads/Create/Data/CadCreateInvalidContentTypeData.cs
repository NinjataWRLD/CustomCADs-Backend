namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.Data;

using CustomCADs.UnitTests.Files.Domain.Cads.Create;
using static CadsData;

public class CadCreateInvalidContentTypeData : CadCreateData
{
    public CadCreateInvalidContentTypeData()
    {
        Add(ValidKey1, InvalidContentType, ValidVolume1, ValidCoord1, ValidCoord1, ValidCoord1);
        Add(ValidKey2, InvalidContentType, ValidVolume2, ValidCoord2, ValidCoord2, ValidCoord2);
    }
}
