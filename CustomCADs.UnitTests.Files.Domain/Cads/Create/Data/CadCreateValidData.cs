namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.Data;

using static CadsData;

public class CadCreateValidData : CadCreateData
{
    public CadCreateValidData()
    {
        Add(ValidKey1, ValidContentType1, ValidCoord1, ValidCoord1, ValidCoord1);
        Add(ValidKey2, ValidContentType2, ValidCoord2, ValidCoord2, ValidCoord2);
    }
}
