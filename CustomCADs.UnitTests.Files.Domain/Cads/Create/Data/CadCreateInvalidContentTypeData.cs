namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.Data;

using static CadsData;

public class CadCreateInvalidContentTypeData : CadCreateData
{
    public CadCreateInvalidContentTypeData()
    {
        Add(ValidKey1, InvalidContentType, ValidCoord1, ValidCoord1, ValidCoord1);
        Add(ValidKey2, InvalidContentType, ValidCoord2, ValidCoord2, ValidCoord2);
    }
}
