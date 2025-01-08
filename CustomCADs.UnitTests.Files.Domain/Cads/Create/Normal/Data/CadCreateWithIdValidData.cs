namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.Normal.Data;

using CustomCADs.UnitTests.Files.Domain.Cads.Create.Normal;
using static CadsData;

public class CadCreateWithIdValidData : CadCreateData
{
    public CadCreateWithIdValidData()
    {
        Add(ValidKey1, ValidContentType1, ValidCoord1, ValidCoord1, ValidCoord1);
        Add(ValidKey2, ValidContentType2, ValidCoord2, ValidCoord2, ValidCoord2);
    }
}
