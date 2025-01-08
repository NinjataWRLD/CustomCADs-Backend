namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.WithId.Data;

using static CadsData;

public class CadCreateInvalidCoordsData : CadCreateWithIdData
{
    public CadCreateInvalidCoordsData()
    {
        Add(ValidKey1, ValidContentType1, InvalidCoord1, InvalidCoord1, InvalidCoord1);
        Add(ValidKey2, ValidContentType2, InvalidCoord2, InvalidCoord2, InvalidCoord2);
    }
}
