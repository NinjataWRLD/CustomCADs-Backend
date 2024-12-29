namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.Data;

public class CadCreateValidData : CadCreateData
{
    public CadCreateValidData()
    {
        Add(CadValidKey1, CadValidContentType1, CadValidCoord1, CadValidCoord1, CadValidCoord1);
        Add(CadValidKey2, CadValidContentType2, CadValidCoord2, CadValidCoord2, CadValidCoord2);
    }
}
