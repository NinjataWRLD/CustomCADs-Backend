namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.Data;

public class CadCreateInvalidContentTypeData : CadCreateData
{
    public CadCreateInvalidContentTypeData()
    {
        Add(CadValidKey1, CadInvalidContentType, CadValidCoord1, CadValidCoord1, CadValidCoord1);
        Add(CadValidKey2, CadInvalidContentType, CadValidCoord2, CadValidCoord2, CadValidCoord2);
    }
}
