namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.Data;

using static CadsData;

public class CadCreateInvalidKeyData : CadCreateData
{
    public CadCreateInvalidKeyData()
    {
        Add(InvalidKey, ValidContentType1, ValidCoord1, ValidCoord1, ValidCoord1);
        Add(InvalidKey, ValidContentType2, ValidCoord2, ValidCoord2, ValidCoord2);
    }
}
