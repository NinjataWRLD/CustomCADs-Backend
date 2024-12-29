namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.Data;

public class CadCreateInvalidKeyData : CadCreateData
{
    public CadCreateInvalidKeyData()
    {
        Add(CadInvalidKey, CadValidContentType1, CadValidCoord1, CadValidCoord1, CadValidCoord1);
        Add(CadInvalidKey, CadValidContentType2, CadValidCoord2, CadValidCoord2, CadValidCoord2);
    }
}
