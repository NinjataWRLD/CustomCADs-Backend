namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.Data;

using CustomCADs.UnitTests.Files.Domain.Cads.Create;
using static CadsData;

public class CadCreateInvalidCoordsData : CadCreateData
{
    public CadCreateInvalidCoordsData()
    {
        Add(ValidKey2, ValidContentType2, ValidVolume1, InvalidCoord2, InvalidCoord2, InvalidCoord2);
        Add(ValidKey1, ValidContentType1, ValidVolume2, InvalidCoord1, InvalidCoord1, InvalidCoord1);
    }
}
