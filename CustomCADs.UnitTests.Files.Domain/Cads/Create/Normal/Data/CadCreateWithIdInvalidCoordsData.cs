namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.Normal.Data;

using CustomCADs.UnitTests.Files.Domain.Cads.Create.Normal;
using static CadsData;

public class CadCreateWithIdInvalidCoordsData : CadCreateData
{
    public CadCreateWithIdInvalidCoordsData()
    {
        Add(ValidKey1, ValidContentType1, InvalidCoord1, InvalidCoord1, InvalidCoord1);
        Add(ValidKey2, ValidContentType2, InvalidCoord2, InvalidCoord2, InvalidCoord2);
    }
}
