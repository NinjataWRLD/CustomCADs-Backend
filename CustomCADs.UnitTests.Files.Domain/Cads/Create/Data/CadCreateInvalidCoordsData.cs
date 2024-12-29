namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.Data;

public class CadCreateInvalidCoordsData : CadCreateData
{
    public CadCreateInvalidCoordsData()
    {
        Add(CadValidKey1, CadValidContentType1, CadInvalidCoord1, CadInvalidCoord1, CadInvalidCoord1);
        Add(CadValidKey2, CadValidContentType2, CadInvalidCoord2, CadInvalidCoord2, CadInvalidCoord2);
    }
}
